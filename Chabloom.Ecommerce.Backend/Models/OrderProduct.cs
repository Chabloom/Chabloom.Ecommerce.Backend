// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.Models
{
    public class OrderProduct
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ulong Amount { get; set; }

        [Required]
        public string CurrencyId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public int Count { get; set; }

        public Guid? ProductId { get; set; }

        public Product Product { get; set; }

        #region Auditing

        [Required]
        public Guid CreatedUser { get; set; } = Guid.Empty;

        [Required]
        public DateTimeOffset CreatedTimestamp { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public Guid UpdatedUser { get; set; } = Guid.Empty;

        [Required]
        public DateTimeOffset UpdatedTimestamp { get; set; } = DateTimeOffset.MinValue;

        #endregion
    }
}