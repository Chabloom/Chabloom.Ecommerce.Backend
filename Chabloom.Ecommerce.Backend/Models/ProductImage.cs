// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Chabloom.Ecommerce.Backend.Models
{
    public class ProductImage
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public string Filename { get; set; }

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