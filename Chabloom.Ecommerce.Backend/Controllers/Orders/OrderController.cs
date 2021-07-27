// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chabloom.Ecommerce.Backend.Data;
using Chabloom.Ecommerce.Backend.Models.Orders;
using Chabloom.Ecommerce.Backend.Services;
using Chabloom.Ecommerce.Backend.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chabloom.Ecommerce.Backend.Controllers.Orders
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrdersController> _logger;
        private readonly IValidator _validator;

        public OrdersController(ApplicationDbContext context, ILogger<OrdersController> logger, IValidator validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetOrdersAsync()
        {
            // Get the user and tenant id
            var (userId, tenantId, userTenantResult) = await GetUserTenantAsync();
            if (!userId.HasValue || !tenantId.HasValue)
            {
                return userTenantResult;
            }

            // Get all orders
            var orders = await _context.Orders
                .Include(x => x.Store)
                .ThenInclude(x => x.Warehouse)
                .Include(x => x.Products)
                .Where(x => x.CreatedUser == userId)
                .Where(x => x.Store.Warehouse.TenantId == tenantId)
                .ToListAsync();

            var viewModels = orders
                .Select(x => new OrderViewModel
                {
                    Id = x.Id,
                    PaymentId = x.PaymentId,
                    PickupMethod = x.PickupMethodName,
                    Status = x.Status,
                    ProductCounts = x.Products
                        .ToDictionary(y => y.ProductId, y => y.Count)
                })
                .ToList();

            return Ok(viewModels);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            // Get the user and tenant id
            var (_, tenantId, userTenantResult) = await GetUserTenantAsync();
            if (!tenantId.HasValue)
            {
                return userTenantResult;
            }

            // Find the specified order
            var order = await _context.Orders
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(order, tenantId.Value);
            if (!tenantValid)
            {
                return tenantResult;
            }

            // Populate the return data
            var viewModel = new OrderViewModel
            {
                Id = order.Id,
                PaymentId = order.PaymentId,
                PickupMethod = order.PickupMethodName,
                Status = order.Status,
                ProductCounts = order.Products
                    .ToDictionary(x => x.ProductId, x => x.Count)
            };

            return Ok(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> PostOrderAsync([FromBody] OrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the user and tenant id
            var (userId, tenantId, userTenantResult) = await GetUserTenantAsync();
            if (!userId.HasValue || !tenantId.HasValue)
            {
                return userTenantResult;
            }

            if (viewModel.ProductCounts.Count == 0)
            {
                _logger.LogWarning($"User {Guid.Empty} attempted to create order with no products");
                return BadRequest("Order must contain products");
            }

            // Create the order
            var order = new Order
            {
                PaymentId = viewModel.PaymentId,
                PickupMethodName = viewModel.PickupMethod,
                StoreId = viewModel.StoreId,
                CreatedUser = userId.Value
            };

            var orderProducts = new List<OrderProduct>();
            foreach (var (productId, count) in viewModel.ProductCounts)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == productId);
                if (product == null)
                {
                    return BadRequest($"Product {productId} does not exist");
                }

                orderProducts.Add(new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Count = count,
                    Name = product.Name,
                    Description = product.Description,
                    Amount = product.Amount,
                    CurrencyId = product.CurrencyId
                });
            }

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(order, tenantId.Value);
            if (!tenantValid)
            {
                return tenantResult;
            }

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            await _context.AddRangeAsync(orderProducts);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new OrderViewModel
            {
                Id = order.Id,
                PaymentId = order.PaymentId,
                PickupMethod = order.PickupMethodName,
                Status = order.Status,
                ProductCounts = order.Products
                    .ToDictionary(x => x.ProductId, x => x.Count)
            };

            _logger.LogInformation($"User {userId} created order {order.Id}");

            return Ok(retViewModel);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] Guid id)
        {
            // Get the user and tenant id
            var (userId, tenantId, userTenantResult) = await GetUserTenantAsync();
            if (!userId.HasValue || !tenantId.HasValue)
            {
                return userTenantResult;
            }

            // Find the specified order
            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                _logger.LogWarning($"Could not find order {id} to delete");
                return NotFound();
            }

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(order, tenantId.Value);
            if (!tenantValid)
            {
                return tenantResult;
            }

            // Ensure the user is authorized at the requested level
            var (roleValid, roleResult) = await ValidateRoleAccessAsync(userId.Value, tenantId.Value);
            if (!roleValid)
            {
                return roleResult;
            }

            order.DisabledUser = userId.Value;
            order.DisabledTimestamp = DateTimeOffset.UtcNow;

            _context.Update(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} disabled order {order.Id}");

            return NoContent();
        }

        private async Task<Tuple<Guid?, Guid?, IActionResult>> GetUserTenantAsync()
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (!userId.HasValue)
            {
                return new Tuple<Guid?, Guid?, IActionResult>(null, null, Forbid());
            }

            // Get the current tenant id
            var tenantId = await _validator.GetTenantIdAsync(Request);
            if (!tenantId.HasValue)
            {
                return new Tuple<Guid?, Guid?, IActionResult>(null, null, Forbid());
            }

            return new Tuple<Guid?, Guid?, IActionResult>(userId, tenantId, Forbid());
        }

        private async Task<Tuple<bool, IActionResult>> ValidateTenantAsync(Order order, Guid tenantId)
        {
            // Find the specified store
            var store = await _context.Stores
                .Include(x => x.Warehouse)
                .FirstOrDefaultAsync(x => x.Id == order.StoreId);
            if (store == null)
            {
                return new Tuple<bool, IActionResult>(false, NotFound());
            }

            // Ensure the user is calling this endpoint from the correct tenant
            if (store.Warehouse.TenantId != tenantId)
            {
                return new Tuple<bool, IActionResult>(false, Forbid());
            }

            return new Tuple<bool, IActionResult>(true, Ok());
        }

        private async Task<Tuple<bool, IActionResult>> ValidateRoleAccessAsync(Guid userId, Guid tenantId)
        {
            // Ensure the user is authorized at the requested level
            var userRoles = await _validator.GetTenantRolesAsync(userId, tenantId);
            if (!userRoles.Contains("Admin") &&
                !userRoles.Contains("Manager"))
            {
                _logger.LogWarning($"User id {userId} was not authorized to perform requested operation");
                return new Tuple<bool, IActionResult>(false, Forbid());
            }

            return new Tuple<bool, IActionResult>(true, Ok());
        }
    }
}