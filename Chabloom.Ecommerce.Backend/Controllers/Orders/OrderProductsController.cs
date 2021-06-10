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

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<List<OrderProductViewModel>>> GetOrderProducts([FromQuery] Guid? orderId)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Populate the return data
            var viewModels = new List<OrderProductViewModel>();
            if (orderId.HasValue)
            {
                viewModels = await _context.OrderProducts
                    .Where(x => x.OrderId == orderId.Value)
                    .Select(x => new OrderProductViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Amount = x.Amount,
                        CurrencyId = x.CurrencyId,
                        OrderId = x.OrderId,
                        Count = x.Count
                    })
                    .ToListAsync();
            }

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<OrderProductViewModel>> GetOrderProduct(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified order product
            var orderProduct = await _context.OrderProducts
                .FirstOrDefaultAsync(x => x.Id == id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            // Populate the return data
            var viewModel = new OrderProductViewModel
            {
                Id = orderProduct.Id,
                Name = orderProduct.Name,
                Description = orderProduct.Description,
                Amount = orderProduct.Amount,
                CurrencyId = orderProduct.CurrencyId,
                OrderId = orderProduct.OrderId,
                Count = orderProduct.Count
            };

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<OrderProductViewModel>> PostOrderProduct(OrderProductViewModel viewModel)
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
                .FirstOrDefaultAsync(x => x.Id == viewModel.OrderId);
            if (order == null)
            {
                _logger.LogWarning($"Could not find order {viewModel.OrderId} to create new order product");
                return BadRequest("Invalid order");
            }

            // Find the specified product
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == viewModel.ProductId);
            if (product == null)
            {
                _logger.LogWarning($"Could not find product {viewModel.ProductId} to create new order product");
                return BadRequest("Invalid product");
            }

            // Create the order product
            var orderProduct = new OrderProduct
            {
                Name = product.Name,
                Description = product.Description,
                Amount = product.Amount,
                CurrencyId = product.CurrencyId,
                OrderId = viewModel.OrderId,
                Count = viewModel.Count
            };

            await _context.AddAsync(orderProduct);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new OrderProductViewModel
            {
                Id = orderProduct.Id,
                Name = orderProduct.Name,
                Description = orderProduct.Description,
                Amount = orderProduct.Amount,
                CurrencyId = orderProduct.CurrencyId,
                OrderId = orderProduct.OrderId,
                Count = orderProduct.Count
            };

            _logger.LogInformation($"User {userId} created order product {orderProduct.Id}");

            return CreatedAtAction("GetOrderProduct", new {id = retViewModel.Id}, retViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteOrderProduct(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified order product
            var orderProduct = await _context.OrderProducts
                .FirstOrDefaultAsync(x => x.Id == id);
            if (orderProduct == null)
            {
                _logger.LogWarning($"Could not find order product {id} to delete");
                return NotFound();
            }

            // Delete the order product
            _context.Remove(orderProduct);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted order product {orderProduct.Id}");

            return NoContent();
        }
    }
}