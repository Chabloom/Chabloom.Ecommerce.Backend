// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.Models.Orders
{
    public class OrderProduct
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ulong Amount { get; set; }

        [Required]
        public string CurrencyId { get; set; }
    }
}