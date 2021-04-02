// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chabloom.Ecommerce.Backend.Models.Inventory
{
    [Table("EcommerceWarehouseProducts")]
    public class WarehouseProduct
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid WarehouseId { get; set; }

        [Required]
        public Warehouse Warehouse { get; set; }

        public Guid? ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public int Count { get; set; }
    }
}