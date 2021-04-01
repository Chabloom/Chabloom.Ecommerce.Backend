// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels.Inventory
{
    public class StoreViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Address { get; set; }

        [Required]
        public Guid TenantId { get; set; }

        public IDictionary<Guid, int> ProductCounts { get; set; }
    }
}