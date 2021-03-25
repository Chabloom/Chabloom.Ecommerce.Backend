// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace Chabloom.Ecommerce.Backend.Services
{
    public class Validator : IValidator
    {
        private readonly ILogger<Validator> _logger;

        public Validator(ILogger<Validator> logger)
        {
            _logger = logger;
        }

        public Guid GetUserId(ClaimsPrincipal user)
        {
            // Get the current user sid
            var sid = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(sid))
            {
                _logger.LogWarning("User attempted call without an sid");
                return Guid.Empty;
            }

            // Ensure the user id can be parsed
            if (!Guid.TryParse(sid, out var userId))
            {
                _logger.LogWarning($"User sid {sid} could not be parsed as Guid");
                return Guid.Empty;
            }

            return userId;
        }

        public string GetUserName(ClaimsPrincipal user)
        {
            // Get the user's name
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}