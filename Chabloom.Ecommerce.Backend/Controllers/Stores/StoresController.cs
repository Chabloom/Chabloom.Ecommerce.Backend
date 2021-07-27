// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chabloom.Ecommerce.Backend.Data;
using Chabloom.Ecommerce.Backend.Services;
using Chabloom.Ecommerce.Backend.ViewModels.Stores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chabloom.Ecommerce.Backend.Controllers.Stores
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator _validator;

        public StoresController(ApplicationDbContext context, IValidator validator)
        {
            _context = context;
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
                    .Include(x => x.Warehouse)
                    .Where(x => x.Warehouse.TenantId == tenantId.Value)
                    .Select(x => new StoreViewModel
                    {
                        Id = x.Id,
                        WarehouseId = x.WarehouseId
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
                .Include(x => x.Warehouse)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            // Populate the return data
            var viewModel = new StoreViewModel
            {
                Id = store.Id,
                WarehouseId = store.WarehouseId
            };

            return Ok(viewModel);
        }
    }
}