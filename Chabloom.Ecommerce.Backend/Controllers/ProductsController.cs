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
    public class ProductsController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly ILogger<ProductsController> _logger;
        private readonly IValidator _validator;

        public ProductsController(EcommerceDbContext context, ILogger<ProductsController> logger, IValidator validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<List<ProductViewModel>>> GetProducts()
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Populate the return data
            var viewModels = await _context.Products
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    CategoryId = x.CategoryId
                })
                .ToListAsync();

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductViewModel>> GetProduct(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified product
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // Populate the return data
            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductViewModel>> PutProduct(Guid id, ProductViewModel viewModel)
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
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                _logger.LogWarning($"Could not find product {viewModel.Id} to update");
                return NotFound();
            }

            // Find the specified category
            var category = await _context.ProductCategories
                .FirstOrDefaultAsync(x => x.Id == viewModel.CategoryId);
            if (category == null)
            {
                _logger.LogWarning($"Could not find category {viewModel.CategoryId} to update product {product.Id}");
                return BadRequest("Invalid category");
            }

            // Update the product
            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.CategoryId = viewModel.CategoryId;
            product.UpdatedUser = userId;
            product.UpdatedTimestamp = DateTimeOffset.UtcNow;

            _context.Update(product);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            _logger.LogInformation($"User {userId} updated product {product.Id}");

            return Ok(retViewModel);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductViewModel>> PostProduct(ProductViewModel viewModel)
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

            // Find the specified category
            var category = await _context.ProductCategories
                .FirstOrDefaultAsync(x => x.Id == viewModel.CategoryId);
            if (category == null)
            {
                _logger.LogWarning($"Could not find category {viewModel.CategoryId} to create new product");
                return BadRequest("Invalid category");
            }

            // Create the product
            var product = new Product
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                CategoryId = viewModel.CategoryId,
                CreatedUser = userId
            };

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            _logger.LogInformation($"User {userId} created product {product.Id}");

            return CreatedAtAction("GetProduct", new {id = retViewModel.Id}, retViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified product
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                _logger.LogWarning($"Could not find product {id} to delete");
                return NotFound();
            }

            // Delete the product
            _context.Remove(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted product {product.Id}");

            return NoContent();
        }
    }
}