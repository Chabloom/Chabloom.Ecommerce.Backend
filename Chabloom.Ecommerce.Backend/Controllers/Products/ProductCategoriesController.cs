// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chabloom.Ecommerce.Backend.Data;
using Chabloom.Ecommerce.Backend.Models.Products;
using Chabloom.Ecommerce.Backend.Services;
using Chabloom.Ecommerce.Backend.ViewModels.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chabloom.Ecommerce.Backend.Controllers.Products
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductCategoriesController> _logger;
        private readonly IValidator _validator;

        public ProductCategoriesController(ApplicationDbContext context, ILogger<ProductCategoriesController> logger,
            IValidator validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetProductCategoriesAsync()
        {
            // Get the current tenant id
            var tenantId = await _validator.GetTenantIdAsync(Request);
            if (!tenantId.HasValue)
            {
                return Forbid();
            }

            var productCategories = await _context.ProductCategories
                .Where(x => x.TenantId == tenantId)
                .ToListAsync();
            if (productCategories.Count == 0)
            {
                return Ok(new List<ProductCategory>());
            }

            // Populate the return data
            var viewModels = productCategories
                .Select(x => new ProductCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    TenantId = x.TenantId,
                    ParentCategoryId = x.ParentCategoryId.GetValueOrDefault()
                })
                .ToList();

            return Ok(viewModels);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetProductCategoryAsync([FromRoute] Guid id)
        {
            // Get the current tenant id
            var tenantId = await _validator.GetTenantIdAsync(Request);
            if (!tenantId.HasValue)
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
            var retViewModel = new ProductCategoryViewModel
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                TenantId = productCategory.TenantId,
                ParentCategoryId = productCategory.ParentCategoryId.GetValueOrDefault()
            };

            return Ok(retViewModel);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutProductCategoryAsync([FromRoute] Guid id,
            [FromBody] ProductCategoryViewModel viewModel)
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

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(productCategory, tenantId.Value);
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

            _context.Update(productCategory);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} updated product category {productCategory.Id}");

            // Populate the return data
            var retViewModel = new ProductCategoryViewModel
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                TenantId = productCategory.TenantId,
                ParentCategoryId = productCategory.ParentCategoryId.GetValueOrDefault()
            };

            return Ok(retViewModel);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> PostProductCategoryAsync([FromBody] ProductCategoryViewModel viewModel)
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
                ParentCategoryId = viewModel.ParentCategoryId
            };

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(productCategory, tenantId.Value);
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

            await _context.AddAsync(productCategory);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} created product category {productCategory.Id}");

            // Populate the return data
            var retViewModel = new ProductCategoryViewModel
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description
            };

            return Ok(retViewModel);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProductCategoryAsync([FromRoute] Guid id)
        {
            // Get the user and tenant id
            var (userId, tenantId, userTenantResult) = await GetUserTenantAsync();
            if (!userId.HasValue || !tenantId.HasValue)
            {
                return userTenantResult;
            }

            // Find the specified product category
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(x => x.Id == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(productCategory, tenantId.Value);
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

            // Delete the product category
            _context.Remove(productCategory);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted product category {productCategory.Id}");

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

        private Task<Tuple<bool, IActionResult>> ValidateTenantAsync(ProductCategory productCategory, Guid tenantId)
        {
            // Ensure the user is calling this endpoint from the correct tenant
            if (productCategory.TenantId != tenantId)
            {
                return Task.FromResult(new Tuple<bool, IActionResult>(false, Forbid()));
            }

            return Task.FromResult(new Tuple<bool, IActionResult>(true, Ok()));
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