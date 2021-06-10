// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels.Stores
{
    public class StoreViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid WarehouseId { get; set; }
    }
}