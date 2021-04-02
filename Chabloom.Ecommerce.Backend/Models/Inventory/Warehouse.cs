// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chabloom.Ecommerce.Backend.Models.Authorization;

namespace Chabloom.Ecommerce.Backend.Models.Inventory
{
    [Table("EcommerceWarehouses")]
    public class Warehouse
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

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

        [Required]
        public Guid UpdatedUser { get; set; } = Guid.Empty;

        [Required]
        public DateTimeOffset UpdatedTimestamp { get; set; } = DateTimeOffset.MinValue;

        #endregion
    }
}