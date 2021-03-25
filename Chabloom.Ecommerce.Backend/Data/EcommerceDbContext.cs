// Copyright 2020-2021 Chabloom LC. All rights reserved.

using Chabloom.Ecommerce.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Chabloom.Ecommerce.Backend.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}