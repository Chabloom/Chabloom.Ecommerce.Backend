// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chabloom.Ecommerce.Backend.Models.Authorization
{
    [Table("EcommerceTenantRoleUsers")]
    public class TenantRoleUser
    {
        [Required]
        public Guid TenantRoleId { get; set; }

        [Required]
        public TenantRole TenantRole { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public User User { get; set; }
    }
}