// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels.Authorization
{
    public class TenantRoleViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid TenantId { get; set; }
    }
}