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
    public class StoreProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StoreProductsController> _logger;
        private readonly IValidator _validator;

        public StoreProductsController(ApplicationDbContext context, ILogger<StoreProductsController> logger,
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
        public async Task<ActionResult<List<StoreProductViewModel>>> GetStoreProducts([FromQuery] Guid? storeId)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Populate the return data
            var viewModels = new List<StoreProductViewModel>();
            if (storeId.HasValue)
            {
                viewModels = await _context.StoreProducts
                    .Where(x => x.StoreId == storeId.Value)
                    .Where(x => x.ProductId.HasValue)
                    .Select(x => new StoreProductViewModel
                    {
                        Id = x.Id,
                        StoreId = x.StoreId,
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
        public async Task<ActionResult<StoreProductViewModel>> GetStoreProduct(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified store product
            var storeProduct = await _context.StoreProducts
                .Where(x => x.ProductId.HasValue)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (storeProduct == null)
            {
                return NotFound();
            }

            // Populate the return data
            var viewModel = new StoreProductViewModel
            {
                Id = storeProduct.Id,
                StoreId = storeProduct.StoreId,
                ProductId = storeProduct.ProductId.GetValueOrDefault(),
                Count = storeProduct.Count
            };

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<StoreProductViewModel>> PutStoreProduct(Guid id, StoreProductViewModel viewModel)
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

            // Find the specified store product
            var storeProduct = await _context.StoreProducts
                .Where(x => x.ProductId.HasValue)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (storeProduct == null)
            {
                _logger.LogWarning($"Could not find store product {viewModel.Id} to update");
                return NotFound();
            }

            // Find the specified store
            var store = await _context.Stores
                .FirstOrDefaultAsync(x => x.Id == viewModel.StoreId);
            if (store == null)
            {
                _logger.LogWarning($"Could not find store {viewModel.StoreId} to create new store product");
                return BadRequest("Invalid store");
            }

            // Find the specified product
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == viewModel.ProductId);
            if (product == null)
            {
                _logger.LogWarning($"Could not find product {viewModel.ProductId} to create new store product");
                return BadRequest("Invalid product");
            }

            // Update the store product
            storeProduct.Count = viewModel.Count;
            storeProduct.UpdatedUser = userId;
            storeProduct.UpdatedTimestamp = DateTimeOffset.UtcNow;

            _context.Update(storeProduct);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new StoreProductViewModel
            {
                Id = storeProduct.Id,
                StoreId = storeProduct.StoreId,
                ProductId = storeProduct.ProductId.GetValueOrDefault(),
                Count = storeProduct.Count
            };

            _logger.LogInformation($"User {userId} updated store product {storeProduct.Id}");

            return Ok(retViewModel);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<StoreProductViewModel>> PostStoreProduct(StoreProductViewModel viewModel)
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

            // Find the specified store
            var store = await _context.Stores
                .FirstOrDefaultAsync(x => x.Id == viewModel.StoreId);
            if (store == null)
            {
                _logger.LogWarning($"Could not find store {viewModel.StoreId} to create new store product");
                return BadRequest("Invalid store");
            }

            // Find the specified product
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == viewModel.ProductId);
            if (product == null)
            {
                _logger.LogWarning($"Could not find product {viewModel.ProductId} to create new store product");
                return BadRequest("Invalid product");
            }

            // Create the store product
            var storeProduct = new StoreProduct
            {
                StoreId = viewModel.StoreId,
                ProductId = viewModel.ProductId,
                Count = viewModel.Count,
                CreatedUser = userId
            };

            await _context.AddAsync(storeProduct);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new StoreProductViewModel
            {
                Id = storeProduct.Id,
                StoreId = storeProduct.StoreId,
                ProductId = storeProduct.ProductId.GetValueOrDefault(),
                Count = storeProduct.Count
            };

            _logger.LogInformation($"User {userId} created store product {storeProduct.Id}");

            return CreatedAtAction("GetStoreProduct", new {id = retViewModel.Id}, retViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteStoreProduct(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified store product
            var storeProduct = await _context.StoreProducts
                .FirstOrDefaultAsync(x => x.Id == id);
            if (storeProduct == null)
            {
                _logger.LogWarning($"Could not find store product {id} to delete");
                return NotFound();
            }

            // Delete the store product
            _context.Remove(storeProduct);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted store product {storeProduct.Id}");

            return NoContent();
        }
    }
}