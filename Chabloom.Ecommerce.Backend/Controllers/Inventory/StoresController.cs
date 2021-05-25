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
    public class StoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StoresController> _logger;
        private readonly IValidator _validator;

        public StoresController(
            ApplicationDbContext context,
            ILogger<StoresController> logger,
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
        public async Task<ActionResult<List<StoreViewModel>>> GetStores([FromQuery] Guid? tenantId)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Populate the return data
            var viewModels = new List<StoreViewModel>();
            if (tenantId.HasValue)
            {
                viewModels = await _context.Stores
                    .Include(x => x.Products)
                    .Where(x => x.TenantId == tenantId.Value)
                    .Select(x => new StoreViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Address = x.Address,
                        TenantId = x.TenantId,
                        ProductCounts = x.Products
                            .ToDictionary(y => y.Id, y => y.Count)
                    })
                    .ToListAsync();
            }

            return Ok(viewModels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<StoreViewModel>> GetStore(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified store
            var store = await _context.Stores
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            // Populate the return data
            var viewModel = new StoreViewModel
            {
                Id = store.Id,
                Name = store.Name,
                Description = store.Description,
                Address = store.Address,
                TenantId = store.TenantId,
                ProductCounts = store.Products
                    .ToDictionary(y => y.Id, y => y.Count)
            };

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<StoreViewModel>> PutStore(Guid id, StoreViewModel viewModel)
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
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (store == null)
            {
                _logger.LogWarning($"Could not find store {viewModel.Id} to update");
                return NotFound();
            }

            // Find the specified tenant
            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(x => x.Id == viewModel.TenantId);
            if (tenant == null)
            {
                _logger.LogWarning($"Could not find tenant {viewModel.TenantId} to update store {store.Id}");
                return BadRequest("Invalid tenant");
            }

            // Update the store
            store.Name = viewModel.Name;
            store.Description = viewModel.Description;
            store.Address = viewModel.Address;
            store.UpdatedUser = userId;
            store.UpdatedTimestamp = DateTimeOffset.UtcNow;

            _context.Update(store);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new StoreViewModel
            {
                Id = store.Id,
                Name = store.Name,
                Description = store.Description,
                Address = store.Address,
                TenantId = store.TenantId,
                ProductCounts = store.Products
                    .ToDictionary(y => y.Id, y => y.Count)
            };

            _logger.LogInformation($"User {userId} updated store {store.Id}");

            return Ok(retViewModel);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<StoreViewModel>> PostStore(StoreViewModel viewModel)
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
                _logger.LogWarning($"Could not find tenant {viewModel.TenantId} to create new store");
                return BadRequest("Invalid tenant");
            }

            // Create the store
            var store = new Store
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Address = viewModel.Address,
                TenantId = viewModel.TenantId,
                CreatedUser = userId
            };

            await _context.AddAsync(store);
            await _context.SaveChangesAsync();

            // Populate the return data
            var retViewModel = new StoreViewModel
            {
                Id = store.Id,
                Name = store.Name,
                Description = store.Description,
                Address = store.Address,
                TenantId = store.TenantId,
                ProductCounts = store.Products
                    .ToDictionary(y => y.Id, y => y.Count)
            };

            _logger.LogInformation($"User {userId} created store {store.Id}");

            return CreatedAtAction("GetStore", new {id = retViewModel.Id}, retViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified store
            var store = await _context.Stores
                .FirstOrDefaultAsync(x => x.Id == id);
            if (store == null)
            {
                _logger.LogWarning($"Could not find store {id} to delete");
                return NotFound();
            }

            // Delete the store
            _context.Remove(store);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {userId} deleted store {store.Id}");

            return NoContent();
        }
    }
}