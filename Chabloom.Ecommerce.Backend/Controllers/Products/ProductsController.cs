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
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<List<ProductViewModel>>> GetProducts([FromQuery] Guid? categoryId,
            [FromQuery] string method)
        {
            // Populate the return data
            var viewModels = new List<ProductViewModel>();
            if (categoryId.HasValue)
            {
                if (!string.IsNullOrEmpty(method))
                {
                    viewModels = await _context.Products
                        .Include(x => x.ProductImages)
                        .Where(x => x.CategoryId == categoryId.Value)
                        .Where(x => x.ProductPickupMethods.Select(y => y.PickupMethodName).Contains(method))
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
                        .ToListAsync();
                }
            }

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ProductViewModel>> GetProduct(Guid id)
        {
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
            var viewModel = new ProductViewModel
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

            _context.Update(product);
            await _context.SaveChangesAsync();

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
                Amount = viewModel.Amount,
                CurrencyId = viewModel.CurrencyId,
                CategoryId = viewModel.CategoryId
            };

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