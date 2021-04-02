// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels.Inventory
{
    public class WarehouseProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid WarehouseId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Count { get; set; }
    }
}