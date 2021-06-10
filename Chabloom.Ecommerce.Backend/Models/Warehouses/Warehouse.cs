// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chabloom.Ecommerce.Backend.Models.Stores;
using Chabloom.Ecommerce.Backend.Models.Tenants;

namespace Chabloom.Ecommerce.Backend.Models.Warehouses
{
    public class Warehouse
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        public Guid? StoreId { get; set; }

        public Store Store { get; set; }

        [Required]
        public Guid TenantId { get; set; }

        [Required]
        public Tenant Tenant { get; set; }

        public List<WarehouseProduct> Products { get; set; }

        #region Auditing

        [Required]
        public Guid CreatedUser { get; set; }

        [Required]
        public DateTimeOffset CreatedTimestamp { get; set; } = DateTimeOffset.UtcNow;

        public Guid? UpdatedUser { get; set; }

        public DateTimeOffset? UpdatedTimestamp { get; set; }

        public Guid? DisabledUser { get; set; }

        public DateTimeOffset? DisabledTimestamp { get; set; }

        #endregion
    }
}