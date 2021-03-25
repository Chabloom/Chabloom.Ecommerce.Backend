// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels
{
    public class ProductImageViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }
    }
}