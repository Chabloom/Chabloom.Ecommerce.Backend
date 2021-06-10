// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using Chabloom.Ecommerce.Backend.Models.Orders;
using Chabloom.Ecommerce.Backend.Models.Products;
using Chabloom.Ecommerce.Backend.Models.Stores;
using Chabloom.Ecommerce.Backend.Models.Tenants;
using Chabloom.Ecommerce.Backend.Models.Warehouses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chabloom.Ecommerce.Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<TenantUser, TenantRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<TenantHost> TenantHosts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPickupMethod> ProductPickupMethods { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<PickupMethod> PickupMethods { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Auth tables

            modelBuilder.Entity<TenantUser>()
                .ToTable("TenantUsers");
            modelBuilder.Entity<TenantRole>()
                .ToTable("TenantRoles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("TenantUserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("TenantUserLogins");
            modelBuilder.Entity<IdentityUserRole<Guid>>()
                .ToTable("TenantUserRoles");
            modelBuilder.Entity<IdentityUserToken<Guid>>()
                .ToTable("TenantUserTokens");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("TenantRoleClaims");

            // Use complex key based on name and tenant id
            modelBuilder.Entity<TenantUser>()
                .HasAlternateKey(x => new {x.UserName, x.TenantId});
            // Use complex key based on name and tenant id
            modelBuilder.Entity<TenantRole>()
                .HasAlternateKey(x => new {x.Name, x.TenantId});

            #endregion

            #region Auth data

            modelBuilder.Entity<Tenant>()
                .HasData(Demo1Data.Tenant);
            modelBuilder.Entity<Tenant>()
                .HasData(Demo2Data.Tenant);
            modelBuilder.Entity<TenantRole>()
                .HasData(Demo1Data.TenantRoles);
            modelBuilder.Entity<TenantRole>()
                .HasData(Demo2Data.TenantRoles);

            #endregion

            #region Application tables

            // Set up subcategories
            modelBuilder.Entity<ProductCategory>()
                .HasOne(x => x.ParentCategory)
                .WithMany(x => x.SubCategories);

            // Set up complex key
            modelBuilder.Entity<ProductImage>()
                .HasKey(x => new {x.Name, x.ProductId});

            // Set up key for join table
            modelBuilder.Entity<WarehouseProduct>()
                .HasKey(x => new {x.WarehouseId, x.ProductId});

            modelBuilder.Entity<ProductPickupMethod>()
                .HasKey(x => new {x.ProductId, x.PickupMethodName});

            #endregion

            #region Application data

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
            modelBuilder.Entity<Store>()
                .HasData(Demo2Data.Stores);

            modelBuilder.Entity<Warehouse>()
                .HasData(Demo1Data.Warehouses);
            modelBuilder.Entity<WarehouseProduct>()
                .HasData(Demo1Data.WarehouseProducts);
            modelBuilder.Entity<Warehouse>()
                .HasData(Demo2Data.Warehouses);

            #endregion
        }
    }
}