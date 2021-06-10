// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chabloom.Ecommerce.Backend.Models.Warehouses;

namespace Chabloom.Ecommerce.Backend.Models.Products
{
    public class Product
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ulong Amount { get; set; }

        [Required]
        public string CurrencyId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public ProductCategory Category { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        public List<ProductPickupMethod> ProductPickupMethods { get; set; }

        public List<WarehouseProduct> Warehouses { get; set; }
    }
}