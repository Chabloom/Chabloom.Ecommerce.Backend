// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chabloom.Ecommerce.Backend.Models.Orders;
using Chabloom.Ecommerce.Backend.Models.Products;

namespace Chabloom.Ecommerce.Backend.Models.Stores
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