// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chabloom.Ecommerce.Backend.Models.Orders;
using Chabloom.Ecommerce.Backend.Models.Warehouses;

namespace Chabloom.Ecommerce.Backend.Models.Stores
{
    public class Store
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Warehouse")]
        public Guid WarehouseId { get; set; }

        [Required]
        public Warehouse Warehouse { get; set; }

        public List<Order> Orders { get; set; }

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