// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using Chabloom.Ecommerce.Backend.Models.Stores;

namespace Chabloom.Ecommerce.Backend.Models.Products
{
    public class ProductPickupMethod
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public string PickupMethodName { get; set; }

        [Required]
        public PickupMethod PickupMethod { get; set; }
    }
}