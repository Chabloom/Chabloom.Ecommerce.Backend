// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels
{
    public class ProductViewModel
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
        public Guid CategoryId { get; set; }

        public List<string> Images { get; set; }

        public List<string> PickupMethods { get; set; }
    }
}