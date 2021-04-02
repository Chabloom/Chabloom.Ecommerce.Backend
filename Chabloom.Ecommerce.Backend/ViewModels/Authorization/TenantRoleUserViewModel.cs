// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels.Authorization
{
    public class TenantRoleUserViewModel
    {
        [Required]
        public Guid TenantRoleId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}