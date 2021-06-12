// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
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
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsController> _logger;
        private readonly IValidator _validator;

        public ProductsController(ApplicationDbContext context, ILogger<ProductsController> logger,
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
        public async Task<IActionResult> GetProductsAsync([FromQuery] Guid categoryId, [FromQuery] string pickupMethod)
        {
            // Get the current tenant id
            var tenantId = await _validator.GetTenantIdAsync(Request);
            if (!tenantId.HasValue)
            {
                return Forbid();
            }

            if (string.IsNullOrEmpty(pickupMethod))
            {
                return Ok(new List<Product>());
            }

            var products = await _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductPickupMethods)
                .Where(x => x.CategoryId == categoryId)
                .Where(x => x.ProductPickupMethods.Select(y => y.PickupMethodName).Contains(pickupMethod))
                .Where(x => x.Category.TenantId == tenantId)
                .ToListAsync();
            if (products.Count == 0)
            {
                return Ok(new List<Product>());
            }

            // Populate the return data
            var viewModels = products
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Amount = x.Amount,
                    CurrencyId = x.CurrencyId,
                    CategoryId = x.CategoryId,
                    Images = x.ProductImages
                        .Select(y => y.Name)
                        .ToList(),
                    PickupMethods = x.ProductPickupMethods
                        .Select(y => y.PickupMethodName)
                        .ToList()
                })
                .ToList();

            return Ok(viewModels);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetProductAsync([FromRoute] Guid id)
        {
            // Get the current tenant id
            var tenantId = await _validator.GetTenantIdAsync(Request);
            if (!tenantId.HasValue)
            {
                return Forbid();
            }

            // Find the specified product
            var product = await _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductPickupMethods)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // Populate the return data
            var retViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Amount = product.Amount,
                CurrencyId = product.CurrencyId,
                CategoryId = product.CategoryId,
                Images = product.ProductImages
                    .Select(y => y.Name)
                    .ToList(),
                PickupMethods = product.ProductPickupMethods
                    .Select(y => y.PickupMethodName)
                    .ToList()
            };

            return Ok(retViewModel);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutProductAsync([FromRoute] Guid id, [FromBody] ProductViewModel viewModel)
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

            // Find the specified product
            var product = await _context.Products
                .Include(x => x.ProductImages)
                .Include(x => x.ProductPickupMethods)
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
            product.Amount = viewModel.Amount;
            product.CurrencyId = viewModel.CurrencyId;
            product.CategoryId = viewModel.CategoryId;

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(product, tenantId.Value);
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

            _context.Update(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} updated product {product.Id}");

            // Populate the return data
            var retViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Amount = product.Amount,
                CurrencyId = product.CurrencyId,
                CategoryId = product.CategoryId,
                Images = product.ProductImages
                    .Select(y => y.Name)
                    .ToList(),
                PickupMethods = product.ProductPickupMethods
                    .Select(y => y.PickupMethodName)
                    .ToList()
            };

            return Ok(retViewModel);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> PostProductAsync([FromBody] ProductViewModel viewModel)
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
                Amount = viewModel.Amount,
                CurrencyId = viewModel.CurrencyId,
                CategoryId = viewModel.CategoryId
            };

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(product, tenantId.Value);
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

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            var productPickupMethods = new List<ProductPickupMethod>();
            foreach (var pickupMethod in viewModel.PickupMethods)
            {
                var productPickupMethod = new ProductPickupMethod
                {
                    ProductId = product.Id,
                    PickupMethodName = pickupMethod
                };
                productPickupMethods.Add(productPickupMethod);
            }

            await _context.AddRangeAsync(productPickupMethods);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} created product {product.Id}");

            // Populate the return data
            var retViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Amount = product.Amount,
                CurrencyId = product.CurrencyId,
                CategoryId = product.CategoryId,
                Images = new List<string>(),
                PickupMethods = productPickupMethods
                    .Select(x => x.PickupMethodName)
                    .ToList()
            };

            return Ok(retViewModel);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id)
        {
            // Get the user and tenant id
            var (userId, tenantId, userTenantResult) = await GetUserTenantAsync();
            if (!userId.HasValue || !tenantId.HasValue)
            {
                return userTenantResult;
            }

            // Find the specified product
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // Validate that the endpoint is called from the correct tenant
            var (tenantValid, tenantResult) = await ValidateTenantAsync(product, tenantId.Value);
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

            // Delete the product
            _context.Remove(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted product {product.Id}");

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

        private async Task<Tuple<bool, IActionResult>> ValidateTenantAsync(Product product, Guid tenantId)
        {
            // Find the specified product category
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(x => x.Id == product.CategoryId);
            if (productCategory == null)
            {
                return new Tuple<bool, IActionResult>(false, NotFound());
            }

            // Ensure the user is calling this endpoint from the correct tenant
            if (productCategory.TenantId != tenantId)
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