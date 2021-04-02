// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chabloom.Ecommerce.Backend.Models
{
    [Table("EcommerceOrderProducts")]
    public class OrderProduct
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Order Order { get; set; }

        public Guid? ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public int Count { get; set; }

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