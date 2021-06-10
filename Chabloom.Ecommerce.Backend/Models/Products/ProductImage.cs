// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.Models.Products
{
    public class ProductImage
    {
        [Required]
        [Key]
        public string Name { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Product Product { get; set; }
    }
}