// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chabloom.Ecommerce.Backend.Models
{
    [Table("EcommercePickupMethods")]
    public class PickupMethod
    {
        [Required]
        [Key]
        [MaxLength(255)]
        public string Name { get; set; }

        public List<Order> Orders { get; set; }

        public List<ProductPickupMethod> ProductPickupMethods { get; set; }
    }
}