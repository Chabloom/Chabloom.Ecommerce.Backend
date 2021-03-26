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
    public class ProductCategoriesController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly ILogger<ProductCategoriesController> _logger;
        private readonly IValidator _validator;

        public ProductCategoriesController(EcommerceDbContext context, ILogger<ProductCategoriesController> logger,
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
        public async Task<ActionResult<List<ProductCategoryViewModel>>> GetProductCategories([FromQuery] Guid? tenantId)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Populate the return data
            var viewModels = new List<ProductCategoryViewModel>();
            if (tenantId.HasValue)
            {
                viewModels = await _context.ProductCategories
                    .Where(x => x.TenantId == tenantId.Value)
                    .Select(x => new ProductCategoryViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        TenantId = x.TenantId,
                        ParentCategoryId = x.ParentCategoryId.GetValueOrDefault()
                    })
                    .ToListAsync();
            }

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductCategoryViewModel>> GetProductCategory(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified product category
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(x => x.Id == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            // Populate the return data
            var viewModel = new ProductCategoryViewModel
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                TenantId = productCategory.TenantId,
                ParentCategoryId = productCategory.ParentCategoryId.GetValueOrDefault()
            };

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductCategoryViewModel>> PutProductCategory(Guid id,
            ProductCategoryViewModel viewModel)
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

            // Find the specified product category
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(x => x.Id == id);
            if (productCategory == null)
            {
                _logger.LogWarning($"Could not find product category {viewModel.Id} to update");
                return NotFound();
            }

            // Find the specified parent category
            var parentCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(x => x.Id == viewModel.ParentCategoryId);
            if (parentCategory == null)
            {
                _logger.LogWarning(
                    $"Could not find parent category {viewModel.ParentCategoryId} for product category {viewModel.Id}");
                return BadRequest("Invalid parent category");
            }

            // Update the product category
            productCategory.Name = viewModel.Name;
            productCategory.Description = viewModel.Description;
            // TenantId cannot be updated
            productCategory.ParentCategoryId = viewModel.ParentCategoryId;
            productCategory.UpdatedUser = userId;
            productCategory.UpdatedTimestamp = DateTimeOffset.UtcNow;

            _context.Update(productCategory);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new ProductCategoryViewModel
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                TenantId = productCategory.TenantId,
                ParentCategoryId = productCategory.ParentCategoryId.GetValueOrDefault()
            };

            _logger.LogInformation($"User {userId} updated product category {productCategory.Id}");

            return Ok(retViewModel);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductCategoryViewModel>> PostProductCategory(
            ProductCategoryViewModel viewModel)
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

            // Find the specified tenant
            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(x => x.Id == viewModel.TenantId);
            if (tenant == null)
            {
                _logger.LogWarning($"Could not find tenant {viewModel.TenantId} to create new product category");
                return BadRequest("Invalid tenant");
            }

            // Find the specified parent category
            var parentCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(x => x.Id == viewModel.ParentCategoryId);
            if (parentCategory == null)
            {
                _logger.LogWarning(
                    $"Could not find parent category {viewModel.ParentCategoryId} for product category {viewModel.Id}");
                return BadRequest("Invalid parent category");
            }

            // Create the product category
            var productCategory = new ProductCategory
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                TenantId = viewModel.TenantId,
                ParentCategoryId = viewModel.ParentCategoryId,
                CreatedUser = userId
            };

            await _context.AddAsync(productCategory);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new ProductCategoryViewModel
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description
            };

            _logger.LogInformation($"User {userId} created product category {productCategory.Id}");

            return CreatedAtAction("GetProductCategory", new {id = retViewModel.Id}, retViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProductCategory(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified product category
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(x => x.Id == id);
            if (productCategory == null)
            {
                _logger.LogWarning($"Could not find product category {id} to delete");
                return NotFound();
            }

            // Delete the product category
            _context.Remove(productCategory);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted product category {productCategory.Id}");

            return NoContent();
        }
    }
}