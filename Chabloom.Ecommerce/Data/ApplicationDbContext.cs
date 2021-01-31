// Copyright 2020 Chabloom LC. All rights reserved.

using Chabloom.Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Chabloom.Ecommerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCost> ProductCosts { get; set; }
    }
}