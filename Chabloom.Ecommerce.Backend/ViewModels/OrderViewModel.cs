// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string PickupMethod { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public Guid TransactionId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public IDictionary<Guid, int> ProductCounts { get; set; }
    }
}