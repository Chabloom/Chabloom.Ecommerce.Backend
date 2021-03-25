// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Security.Claims;

namespace Chabloom.Ecommerce.Backend.Services
{
    public interface IValidator
    {
        /// <summary>
        ///     Validate that a user has a user id
        /// </summary>
        /// <param name="user">The claims principal to check</param>
        /// <returns>The user id or an empty guid on failure</returns>
        public Guid GetUserId(ClaimsPrincipal user);
    }
}