// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels
{
    public class OrderProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        public Guid? ProductId { get; set; }

        [Required]
        public int Count { get; set; }
    }
}