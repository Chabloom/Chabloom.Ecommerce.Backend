// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;
using Chabloom.Ecommerce.Backend.Data;
using Chabloom.Ecommerce.Backend.Models.Products;
using Chabloom.Ecommerce.Backend.Services;
using Chabloom.Ecommerce.Backend.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chabloom.Ecommerce.Backend.Controllers.Products
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductImagesController> _logger;
        private readonly IValidator _validator;

        public ProductImagesController(ApplicationDbContext context, ILogger<ProductImagesController> logger,
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
        public async Task<IActionResult> CreateProductImageAsync([FromBody] ProductImageViewModel viewModel)
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

            // Ensure the user is authorized at the requested level
            var (roleValid, roleResult) = await ValidateRoleAccessAsync(userId.Value, tenantId.Value);
            if (!roleValid)
            {
                return roleResult;
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
                Name = viewModel.Name,
                ProductId = viewModel.ProductId
            };

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(productImage, tenantId.Value);
            if (!tenantValid)
            {
                return tenantResult;
            }

            await _context.AddAsync(productImage);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                $"User {userId} added product image {productImage.Name} for product {productImage.ProductId}");

            return Ok();
        }

        [HttpPost("Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProductImageAsync([FromBody] ProductImageViewModel viewModel)
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

            // Ensure the user is authorized at the requested level
            var (roleValid, roleResult) = await ValidateRoleAccessAsync(userId.Value, tenantId.Value);
            if (!roleValid)
            {
                return roleResult;
            }

            // Find the specified product image
            var productImage = await _context.ProductImages
                .Where(x => x.ProductId == viewModel.ProductId)
                .FirstOrDefaultAsync(x => x.Name == viewModel.Name);
            if (productImage == null)
            {
                return NotFound();
            }

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(productImage, tenantId.Value);
            if (!tenantValid)
            {
                return tenantResult;
            }

            _context.Remove(productImage);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                $"User {userId} deleted product image {productImage.Name} for product {productImage.ProductId}");

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

        private async Task<Tuple<bool, IActionResult>> ValidateTenantAsync(ProductImage productImage, Guid tenantId)
        {
            // Find the specified product
            var product = await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == productImage.ProductId);
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