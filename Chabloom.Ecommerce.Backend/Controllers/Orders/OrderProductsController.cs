// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
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
    public class OrderProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderProductsController> _logger;
        private readonly IValidator _validator;

        public OrderProductsController(ApplicationDbContext context, ILogger<OrderProductsController> logger,
            IValidator validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateOrderProductAsync([FromBody] OrderProductViewModel viewModel)
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

            // Find the specified order
            var order = await _context.Orders
                .FindAsync(viewModel.OrderId);
            if (order == null)
            {
                return BadRequest("Invalid order");
            }

            // Ensure the order has not been processed
            if (order.Status != "Pending")
            {
                return Forbid();
            }

            // Check if the user modifying the products created the order
            if (order.CreatedUser != userId)
            {
                // Ensure the user is authorized at the requested level
                var (roleValid, roleResult) = await ValidateRoleAccessAsync(userId.Value, tenantId.Value);
                if (!roleValid)
                {
                    return roleResult;
                }
            }

            // Find the specified product
            var product = await _context.Products
                .FindAsync(viewModel.ProductId);
            if (product == null)
            {
                return BadRequest("Invalid product");
            }

            // Create the order product
            var orderProduct = new OrderProduct
            {
                OrderId = viewModel.OrderId,
                ProductId = viewModel.ProductId,
                Count = viewModel.Count,
                Name = product.Name,
                Description = product.Description,
                Amount = product.Amount,
                CurrencyId = product.CurrencyId
            };

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(orderProduct, tenantId.Value);
            if (!tenantValid)
            {
                return tenantResult;
            }

            await _context.AddAsync(orderProduct);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                $"User {userId} added product {orderProduct.ProductId} to order {orderProduct.OrderId}");

            return Ok();
        }

        [HttpPost("Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteOrderProductAsync([FromBody] OrderProductViewModel viewModel)
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

            // Find the specified order
            var order = await _context.Orders
                .FindAsync(viewModel.OrderId);
            if (order == null)
            {
                return BadRequest("Invalid order");
            }

            // Ensure the order has not been processed
            if (order.Status != "Pending")
            {
                return Forbid();
            }

            // Check if the user modifying the products created the order
            if (order.CreatedUser != userId)
            {
                // Ensure the user is authorized at the requested level
                var (roleValid, roleResult) = await ValidateRoleAccessAsync(userId.Value, tenantId.Value);
                if (!roleValid)
                {
                    return roleResult;
                }
            }

            // Find the specified order product
            var orderProduct = await _context.OrderProducts
                .FindAsync(viewModel.OrderId, viewModel.ProductId);
            if (orderProduct == null)
            {
                return NotFound();
            }

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(orderProduct, tenantId.Value);
            if (!tenantValid)
            {
                return tenantResult;
            }

            _context.Remove(orderProduct);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                $"User {userId} deleted product {orderProduct.ProductId} from order {orderProduct.OrderId}");

            return Ok();
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

        private async Task<Tuple<bool, IActionResult>> ValidateTenantAsync(OrderProduct orderProduct, Guid tenantId)
        {
            // Find the specified product
            var product = await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == orderProduct.ProductId);
            if (product == null)
            {
                return new Tuple<bool, IActionResult>(false, NotFound());
            }

            // Ensure the user is calling this endpoint from the correct tenant
            if (product.Category.TenantId != tenantId)
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