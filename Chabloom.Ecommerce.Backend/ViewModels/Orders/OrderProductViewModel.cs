// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels.Orders
{
    public class OrderProductViewModel
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ulong Amount { get; set; }

        public string CurrencyId { get; set; }
    }
}