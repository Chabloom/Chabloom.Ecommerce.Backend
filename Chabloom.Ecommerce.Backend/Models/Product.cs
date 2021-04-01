// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chabloom.Ecommerce.Backend.Models.Inventory;

namespace Chabloom.Ecommerce.Backend.Models
{
    [Table("EcommerceProducts")]
    public class Product
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
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public ProductCategory Category { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        public List<OrderProduct> Orders { get; set; }

        public List<StoreProduct> Stores { get; set; }

        public List<WarehouseProduct> Warehouses { get; set; }

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