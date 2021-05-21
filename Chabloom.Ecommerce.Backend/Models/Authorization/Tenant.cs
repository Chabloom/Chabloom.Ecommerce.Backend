// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chabloom.Ecommerce.Backend.Models.Inventory;

namespace Chabloom.Ecommerce.Backend.Models.Authorization
{
    public class Tenant
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public List<TenantRole> TenantRoles { get; set; }

        public List<Store> Stores { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        #region Auditing

        [Required]
        public Guid CreatedUser { get; set; } = Guid.Empty;

        [Required]
        public DateTimeOffset CreatedTimestamp { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public Guid UpdatedUser { get; set; } = Guid.Empty;

        [Required]
        public DateTimeOffset UpdatedTimestamp { get; set; } = DateTimeOffset.MinValue;

        #endregion
    }
}