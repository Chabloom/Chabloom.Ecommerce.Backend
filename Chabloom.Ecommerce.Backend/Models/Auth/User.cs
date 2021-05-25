﻿// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Chabloom.Ecommerce.Backend.Models.Auth
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public Guid TenantId { get; set; }
    }
}