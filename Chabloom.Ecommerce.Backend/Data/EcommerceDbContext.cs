// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.Collections.Generic;
using Chabloom.Ecommerce.Backend.Models;
using Chabloom.Ecommerce.Backend.Models.Authorization;
using Chabloom.Ecommerce.Backend.Models.Inventory;
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

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<PickupMethod> PickupMethods { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<TenantRole> TenantRoles { get; set; }

        public DbSet<TenantRoleUser> TenantRoleUsers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<StoreProduct> StoreProducts { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set up key for join table
            modelBuilder.Entity<TenantRoleUser>()
                .HasKey(x => new {x.TenantRoleId, x.UserId});

            // Set up subcategories
            modelBuilder.Entity<ProductCategory>()
                .HasOne(x => x.ParentCategory)
                .WithMany(x => x.SubCategories);

            // Set up key for join table
            modelBuilder.Entity<OrderProduct>()
                .HasIndex(x => new {x.OrderId, x.ProductId})
                .IsUnique();

            // Set up key for join table
            modelBuilder.Entity<StoreProduct>()
                .HasIndex(x => new {x.StoreId, x.ProductId})
                .IsUnique();

            // Set up key for join table
            modelBuilder.Entity<WarehouseProduct>()
                .HasIndex(x => new {x.WarehouseId, x.ProductId})
                .IsUnique();

            modelBuilder.Entity<ProductPickupMethod>()
                .HasKey(x => new {x.ProductId, x.PickupMethodName});

            // Set up default roles
            var roles = new List<Role>
            {
                new()
                {
                    Name = "Admin"
                },
                new()
                {
                    Name = "Manager"
                }
            };

            modelBuilder.Entity<Role>()
                .HasData(roles);

            // Set up order methods
            var orderMethods = new List<PickupMethod>
            {
                new()
                {
                    Name = "In-Store"
                },
                new()
                {
                    Name = "Pickup"
                },
                new()
                {
                    Name = "Delivery"
                },
                new()
                {
                    Name = "Shipping"
                }
            };

            modelBuilder.Entity<PickupMethod>()
                .HasData(orderMethods);

            modelBuilder.Entity<Tenant>()
                .HasData(Demo1Data.Tenant);
            modelBuilder.Entity<Tenant>()
                .HasData(Demo2Data.Tenant);

            modelBuilder.Entity<TenantRole>()
                .HasData(Demo1Data.TenantRoles);
            modelBuilder.Entity<TenantRole>()
                .HasData(Demo2Data.TenantRoles);

            modelBuilder.Entity<ProductCategory>()
                .HasData(Demo1Data.ProductCategories);
            modelBuilder.Entity<ProductCategory>()
                .HasData(Demo2Data.ProductCategories);

            modelBuilder.Entity<Product>()
                .HasData(Demo1Data.Products);
            modelBuilder.Entity<Product>()
                .HasData(Demo2Data.Products);

            modelBuilder.Entity<ProductImage>()
                .HasData(Demo1Data.ProductImages);
            modelBuilder.Entity<ProductImage>()
                .HasData(Demo2Data.ProductImages);

            modelBuilder.Entity<ProductPickupMethod>()
                .HasData(Demo1Data.ProductPickupMethods);
            modelBuilder.Entity<ProductPickupMethod>()
                .HasData(Demo2Data.ProductPickupMethods);

            modelBuilder.Entity<Store>()
                .HasData(Demo1Data.Stores);

            modelBuilder.Entity<StoreProduct>()
                .HasData(Demo1Data.StoreProducts);

            modelBuilder.Entity<Warehouse>()
                .HasData(Demo1Data.Warehouses);

            modelBuilder.Entity<WarehouseProduct>()
                .HasData(Demo1Data.WarehouseProducts);
        }
    }
}