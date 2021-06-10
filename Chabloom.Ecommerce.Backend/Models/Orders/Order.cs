// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chabloom.Ecommerce.Backend.Models.Stores;

namespace Chabloom.Ecommerce.Backend.Models.Orders
{
    public class Order
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string PaymentId { get; set; }

        [Required]
        public string PickupMethodName { get; set; }

        [Required]
        public PickupMethod PickupMethod { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public Guid StoreId { get; set; }

        [Required]
        public Store Store { get; set; }

        public List<OrderProduct> Products { get; set; }

        #region Auditing

        [Required]
        public Guid CreatedUser { get; set; }

        [Required]
        public DateTimeOffset CreatedTimestamp { get; set; } = DateTimeOffset.UtcNow;

        public Guid? UpdatedUser { get; set; }

        public DateTimeOffset? UpdatedTimestamp { get; set; }

        public Guid? DisabledUser { get; set; }

        public DateTimeOffset? DisabledTimestamp { get; set; }

        #endregion
    }
}