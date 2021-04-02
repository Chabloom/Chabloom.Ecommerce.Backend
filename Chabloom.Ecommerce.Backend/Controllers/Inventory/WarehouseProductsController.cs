// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chabloom.Ecommerce.Backend.Data;
using Chabloom.Ecommerce.Backend.Models.Inventory;
using Chabloom.Ecommerce.Backend.Services;
using Chabloom.Ecommerce.Backend.ViewModels.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chabloom.Ecommerce.Backend.Controllers.Inventory
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WarehouseProductsController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly ILogger<WarehouseProductsController> _logger;
        private readonly IValidator _validator;

        public WarehouseProductsController(EcommerceDbContext context, ILogger<WarehouseProductsController> logger,
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
        public async Task<ActionResult<List<WarehouseProductViewModel>>> GetWarehouseProducts(
            [FromQuery] Guid? warehouseId)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Populate the return data
            var viewModels = new List<WarehouseProductViewModel>();
            if (warehouseId.HasValue)
            {
                viewModels = await _context.WarehouseProducts
                    .Where(x => x.WarehouseId == warehouseId.Value)
                    .Where(x => x.ProductId.HasValue)
                    .Select(x => new WarehouseProductViewModel
                    {
                        Id = x.Id,
                        WarehouseId = x.WarehouseId,
                        ProductId = x.ProductId.Value,
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
        public async Task<ActionResult<WarehouseProductViewModel>> GetWarehouseProduct(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified warehouse product
            var warehouseProduct = await _context.WarehouseProducts
                .Where(x => x.ProductId.HasValue)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (warehouseProduct == null)
            {
                return NotFound();
            }

            // Populate the return data
            var viewModel = new WarehouseProductViewModel
            {
                Id = warehouseProduct.Id,
                WarehouseId = warehouseProduct.WarehouseId,
                ProductId = warehouseProduct.ProductId.GetValueOrDefault(),
                Count = warehouseProduct.Count
            };

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<WarehouseProductViewModel>> PutWarehouseProduct(Guid id,
            WarehouseProductViewModel viewModel)
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

            // Find the specified warehouse product
            var warehouseProduct = await _context.WarehouseProducts
                .Where(x => x.ProductId.HasValue)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (warehouseProduct == null)
            {
                _logger.LogWarning($"Could not find warehouse product {viewModel.Id} to update");
                return NotFound();
            }

            // Find the specified warehouse
            var warehouse = await _context.Warehouses
                .FirstOrDefaultAsync(x => x.Id == viewModel.WarehouseId);
            if (warehouse == null)
            {
                _logger.LogWarning($"Could not find warehouse {viewModel.WarehouseId} to create new warehouse product");
                return BadRequest("Invalid warehouse");
            }

            // Find the specified product
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == viewModel.ProductId);
            if (product == null)
            {
                _logger.LogWarning($"Could not find product {viewModel.ProductId} to create new warehouse product");
                return BadRequest("Invalid product");
            }

            // Update the warehouse product
            warehouseProduct.Count = viewModel.Count;
            warehouseProduct.UpdatedUser = userId;
            warehouseProduct.UpdatedTimestamp = DateTimeOffset.UtcNow;

            _context.Update(warehouseProduct);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new WarehouseProductViewModel
            {
                Id = warehouseProduct.Id,
                WarehouseId = warehouseProduct.WarehouseId,
                ProductId = warehouseProduct.ProductId.GetValueOrDefault(),
                Count = warehouseProduct.Count
            };

            _logger.LogInformation($"User {userId} updated warehouse product {warehouseProduct.Id}");

            return Ok(retViewModel);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<WarehouseProductViewModel>> PostWarehouseProduct(
            WarehouseProductViewModel viewModel)
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

            // Find the specified warehouse
            var warehouse = await _context.Warehouses
                .FirstOrDefaultAsync(x => x.Id == viewModel.WarehouseId);
            if (warehouse == null)
            {
                _logger.LogWarning($"Could not find warehouse {viewModel.WarehouseId} to create new warehouse product");
                return BadRequest("Invalid warehouse");
            }

            // Find the specified product
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == viewModel.ProductId);
            if (product == null)
            {
                _logger.LogWarning($"Could not find product {viewModel.ProductId} to create new warehouse product");
                return BadRequest("Invalid product");
            }

            // Create the warehouse product
            var warehouseProduct = new WarehouseProduct
            {
                WarehouseId = viewModel.WarehouseId,
                ProductId = viewModel.ProductId,
                Count = viewModel.Count,
                CreatedUser = userId
            };

            await _context.AddAsync(warehouseProduct);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new WarehouseProductViewModel
            {
                Id = warehouseProduct.Id,
                WarehouseId = warehouseProduct.WarehouseId,
                ProductId = warehouseProduct.ProductId.GetValueOrDefault(),
                Count = warehouseProduct.Count
            };

            _logger.LogInformation($"User {userId} created warehouse product {warehouseProduct.Id}");

            return CreatedAtAction("GetWarehouseProduct", new {id = retViewModel.Id}, retViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteWarehouseProduct(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified warehouse product
            var warehouseProduct = await _context.WarehouseProducts
                .FirstOrDefaultAsync(x => x.Id == id);
            if (warehouseProduct == null)
            {
                _logger.LogWarning($"Could not find warehouse product {id} to delete");
                return NotFound();
            }

            // Delete the warehouse product
            _context.Remove(warehouseProduct);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted warehouse product {warehouseProduct.Id}");

            return NoContent();
        }
    }
}