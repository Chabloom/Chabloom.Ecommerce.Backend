// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using Chabloom.Ecommerce.Backend.Models.Products;

namespace Chabloom.Ecommerce.Backend.Models.Warehouses
{
    public class WarehouseProduct
    {
        [Required]
        public Guid WarehouseId { get; set; }

        [Required]
        public Warehouse Warehouse { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public int Count { get; set; }
    }
}