// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.Models
{
    public class PickupMethod
    {
        [Required]
        [Key]
        public string Name { get; set; }

        public List<Order> Orders { get; set; }

        public List<ProductPickupMethod> ProductPickupMethods { get; set; }
    }
}