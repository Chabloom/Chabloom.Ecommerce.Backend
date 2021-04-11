// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chabloom.Ecommerce.Backend.Models.Authorization;

namespace Chabloom.Ecommerce.Backend.Models
{
    [Table("EcommerceOrders")]
    public class Order
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string PickupMethodName { get; set; }

        [Required]
        public PickupMethod PickupMethod { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public Guid TransactionId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public User User { get; set; }

        public List<OrderProduct> Products { get; set; }

        #region Auditing

        [Required]
        public Guid CreatedUser { get; set; }

        [Required]
        public DateTimeOffset CreatedTimestamp { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public Guid UpdatedUser { get; set; } = Guid.Empty;

        [Required]
        public DateTimeOffset UpdatedTimestamp { get; set; } = DateTimeOffset.MinValue;

        #endregion
    }
}