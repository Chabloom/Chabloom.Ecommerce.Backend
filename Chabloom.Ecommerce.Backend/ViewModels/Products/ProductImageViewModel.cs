// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels.Products
{
    public class ProductImageViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid ProductId { get; set; }
    }
}