// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chabloom.Ecommerce.Backend.Models
{
    [Table("EcommerceOrderProducts")]
    public class OrderProduct
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public int Count { get; set; }
    }
}