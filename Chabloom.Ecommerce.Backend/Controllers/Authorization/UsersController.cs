// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Threading.Tasks;
using Chabloom.Ecommerce.Backend.Data;
using Chabloom.Ecommerce.Backend.Models.Authorization;
using Chabloom.Ecommerce.Backend.Services;
using Chabloom.Ecommerce.Backend.ViewModels.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chabloom.Ecommerce.Backend.Controllers.Authorization
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly IValidator _validator;

        public UsersController(EcommerceDbContext context, ILogger<UsersController> logger,
            IValidator validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<UserViewModel>> GetUser(Guid id)
        {
            // Get the user id
            var userId = _validator.GetUserId(User);
            if (userId == Guid.Empty)
            {
                return Forbid();
            }

            // Find the specified user
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                if (userId != id)
                {
                    return NotFound();
                }

                // Create the new user
                user = new User
                {
                    Id = userId,
                    Name = _validator.GetUserName(User),
                    CreatedUser = userId
                };

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"User {user.Id} was created");
            }

            // Populate the return data
            var viewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                RoleName = user.RoleName
            };

            return Ok(viewModel);
        }
    }
}