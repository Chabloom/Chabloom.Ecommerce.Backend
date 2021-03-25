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
    public class ProductImagesController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly ILogger<ProductImagesController> _logger;
        private readonly IValidator _validator;

        public ProductImagesController(EcommerceDbContext context, ILogger<ProductImagesController> logger,
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
        public async Task<ActionResult<List<ProductImageViewModel>>> GetProductImages([FromQuery] Guid? productId)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Populate the return data
            List<ProductImageViewModel> viewModels;
            if (productId.HasValue)
            {
                viewModels = await _context.ProductImages
                    .Where(x => x.ProductId == productId.Value)
                    .Select(x => new ProductImageViewModel
                    {
                        Id = x.Id,
                        ProductId = x.ProductId
                    })
                    .ToListAsync();
            }
            else
            {
                viewModels = await _context.ProductImages
                    .Select(x => new ProductImageViewModel
                    {
                        Id = x.Id,
                        ProductId = x.ProductId
                    })
                    .ToListAsync();
            }

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductImageViewModel>> GetProductImage(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified product image
            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(x => x.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            // Populate the return data
            var viewModel = new ProductImageViewModel
            {
                Id = productImage.Id,
                ProductId = productImage.ProductId
            };

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductImageViewModel>> PutProductImage(Guid id,
            ProductImageViewModel viewModel)
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

            // Find the specified product image
            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(x => x.Id == id);
            if (productImage == null)
            {
                _logger.LogWarning($"Could not find product image {viewModel.Id} to update");
                return NotFound();
            }

            // Update the product image
            // ProductId cannot be updated
            productImage.UpdatedUser = userId;
            productImage.UpdatedTimestamp = DateTimeOffset.UtcNow;

            _context.Update(productImage);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new ProductImageViewModel
            {
                Id = productImage.Id,
                ProductId = productImage.ProductId
            };

            _logger.LogInformation($"User {userId} updated product image {productImage.Id}");

            return Ok(retViewModel);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductImageViewModel>> PostProductImage(
            ProductImageViewModel viewModel)
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

            // Find the specified product
            var tenant = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == viewModel.ProductId);
            if (tenant == null)
            {
                _logger.LogWarning($"Could not find product {viewModel.ProductId} to create new product image");
                return BadRequest("Invalid product");
            }

            // Create the product image
            var productImage = new ProductImage
            {
                ProductId = viewModel.ProductId,
                CreatedUser = userId
            };

            await _context.AddAsync(productImage);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new ProductImageViewModel
            {
                Id = productImage.Id,
                ProductId = productImage.ProductId
            };

            _logger.LogInformation($"User {userId} created product image {productImage.Id}");

            return CreatedAtAction("GetProductImage", new {id = retViewModel.Id}, retViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProductImage(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified product image
            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(x => x.Id == id);
            if (productImage == null)
            {
                _logger.LogWarning($"Could not find product image {id} to delete");
                return NotFound();
            }

            // Delete the product image
            _context.Remove(productImage);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted product image {productImage.Id}");

            return NoContent();
        }
    }
}