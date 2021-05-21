// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels
{
    public class OrderProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ulong Amount { get; set; }

        [Required]
        public string CurrencyId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        public Guid? ProductId { get; set; }
    }
}