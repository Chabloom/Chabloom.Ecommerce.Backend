// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chabloom.Ecommerce.Backend.Models.Tenants;

namespace Chabloom.Ecommerce.Backend.Models.Products
{
    public class ProductCategory
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid TenantId { get; set; }

        [Required]
        public Tenant Tenant { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public ProductCategory ParentCategory { get; set; }

        public List<ProductCategory> SubCategories { get; set; }

        public List<Product> Products { get; set; }
    }
}