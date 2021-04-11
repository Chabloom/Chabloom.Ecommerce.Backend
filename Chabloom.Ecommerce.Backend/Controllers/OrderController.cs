// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chabloom.Ecommerce.Backend.Data;
using Chabloom.Ecommerce.Backend.Models;
using Chabloom.Ecommerce.Backend.Services;
using Chabloom.Ecommerce.Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chabloom.Ecommerce.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly ILogger<OrdersController> _logger;
        private readonly IValidator _validator;

        public OrdersController(EcommerceDbContext context, ILogger<OrdersController> logger, IValidator validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<List<OrderViewModel>>> GetOrders([FromQuery] Guid? userId)
        {
            // Get the user id
            var curUserId = _validator.GetUserId(User);
            if (curUserId == Guid.Empty)
            {
                return Forbid();
            }

            // Populate the return data
            List<OrderViewModel> viewModels;
            if (userId.HasValue)
            {
                viewModels = await _context.Orders
                    .Include(x => x.Products)
                    .Where(x => x.UserId == userId.Value)
                    .Select(x => new OrderViewModel
                    {
                        Id = x.Id,
                        PickupMethod = x.PickupMethodName,
                        Status = x.Status,
                        UserId = x.UserId,
                        TransactionId = x.TransactionId,
                        ProductCounts = x.Products
                            .ToDictionary(y => y.Id, y => y.Count)
                    })
                    .ToListAsync();
            }
            else
            {
                viewModels = await _context.Orders
                    .Include(x => x.Products)
                    .Select(x => new OrderViewModel
                    {
                        Id = x.Id,
                        PickupMethod = x.PickupMethodName,
                        Status = x.Status,
                        UserId = x.UserId,
                        TransactionId = x.TransactionId,
                        ProductCounts = x.Products
                            .ToDictionary(y => y.Id, y => y.Count)
                    })
                    .ToListAsync();
            }

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<OrderViewModel>> GetOrder(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified order
            var order = await _context.Orders
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            // Populate the return data
            var viewModel = new OrderViewModel
            {
                Id = order.Id,
                PickupMethod = order.PickupMethodName,
                Status = order.Status,
                UserId = order.UserId,
                TransactionId = order.TransactionId,
                ProductCounts = order.Products
                    .ToDictionary(x => x.Id, x => x.Count)
            };

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<OrderViewModel>> PutOrder(Guid id, OrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified order
            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                _logger.LogWarning($"Could not find order {viewModel.Id} to update");
                return NotFound();
            }

            // Find the specified pickup method
            var pickupMethod = await _context.PickupMethods
                .FirstOrDefaultAsync(x => x.Name == viewModel.PickupMethod);
            if (pickupMethod == null)
            {
                _logger.LogWarning($"Could not find pickup method {viewModel.PickupMethod} to update order {order.Id}");
                return BadRequest("Invalid pickup method");
            }

            // Update the order
            order.PickupMethodName = viewModel.PickupMethod;
            order.Status = viewModel.Status;
            order.TransactionId = viewModel.TransactionId;
            order.UpdatedUser = userId;
            order.UpdatedTimestamp = DateTimeOffset.UtcNow;

            _context.Update(order);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new OrderViewModel
            {
                Id = order.Id,
                PickupMethod = order.PickupMethodName,
                Status = order.Status,
                TransactionId = order.TransactionId,
                ProductCounts = order.Products
                    .ToDictionary(x => x.Id, x => x.Count)
            };

            _logger.LogInformation($"User {userId} updated order {order.Id}");

            return Ok(retViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<OrderViewModel>> PostOrder(OrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Create the order
            var order = new Order
            {
                PickupMethodName = viewModel.PickupMethod,
                TransactionId = viewModel.TransactionId,
                CreatedUser = userId
            };

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            try
            {
                // Create the order products
                var orderProducts = await _context.Products
                    .Where(x => viewModel.ProductCounts.ContainsKey(x.Id))
                    .Select(x => new OrderProduct
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        ProductId = x.Id,
                        Count = viewModel.ProductCounts[x.Id],
                        CreatedUser = userId
                    })
                    .ToListAsync();

                await _context.AddAsync(orderProducts);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                _logger.LogWarning($"User {userId} attempted to create order with bad products");

                _context.Remove(order);
                await _context.SaveChangesAsync();

                return BadRequest("Invalid order products");
            }

            // Populate the return data
            var retViewModel = new OrderViewModel
            {
                Id = order.Id,
                PickupMethod = order.PickupMethodName,
                Status = order.Status,
                TransactionId = order.TransactionId,
                ProductCounts = order.Products
                    .ToDictionary(x => x.Id, x => x.Count)
            };

            _logger.LogInformation($"User {userId} created order {order.Id}");

            return CreatedAtAction("GetOrder", new {id = retViewModel.Id}, retViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified order
            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                _logger.LogWarning($"Could not find order {id} to delete");
                return NotFound();
            }

            // Delete the order
            _context.Remove(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted order {order.Id}");

            return NoContent();
        }
    }
}