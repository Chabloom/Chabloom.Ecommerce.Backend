// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chabloom.Ecommerce.Backend.Models.Inventory
{
    [Table("EcommerceStoreProducts")]
    public class StoreProduct
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid StoreId { get; set; }

        [Required]
        public Store Store { get; set; }

        public Guid? ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public int Count { get; set; }
    }
}