// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using Chabloom.Ecommerce.Backend.Models.Orders;
using Chabloom.Ecommerce.Backend.Models.Products;
using Chabloom.Ecommerce.Backend.Models.Stores;
using Chabloom.Ecommerce.Backend.Models.Tenants;
using Chabloom.Ecommerce.Backend.Models.Warehouses;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chabloom.Ecommerce.Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<TenantUser, TenantRole, Guid>, IDataProtectionKeyContext
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

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Auth tables

            modelBuilder.Entity<TenantUser>(builder =>
            {
                // Make predefined index non-unique
                builder.HasIndex(x => x.NormalizedUserName)
                    .HasDatabaseName("UserNameIndex")
                    .IsUnique(false);
                // Add tenant-based key
                builder.HasAlternateKey(x => new {x.NormalizedUserName, x.TenantId});
                builder.ToTable("TenantUsers");
            });
            modelBuilder.Entity<TenantRole>(builder =>
            {
                // Make predefined index non-unique
                builder.HasIndex(x => x.NormalizedName)
                    .HasDatabaseName("RoleNameIndex")
                    .IsUnique(false);
                // Add tenant-based key
                builder.HasAlternateKey(x => new {x.NormalizedName, x.TenantId});
                builder.ToTable("TenantRoles");
            });
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

            #endregion

            #region Auth data

            modelBuilder.Entity<Tenant>(builder =>
            {
                builder.HasData(Demo1Data.Tenant);
                builder.HasData(Demo2Data.Tenant);
            });
            modelBuilder.Entity<TenantRole>(builder =>
            {
                builder.HasData(Demo1Data.TenantRoles);
                builder.HasData(Demo2Data.TenantRoles);
            });
            modelBuilder.Entity<TenantUser>(builder =>
            {
                builder.HasData(Demo1Data.TenantUsers);
                builder.HasData(Demo2Data.TenantUsers);
            });
            modelBuilder.Entity<IdentityUserClaim<Guid>>(builder =>
            {
                builder.HasData(Demo1Data.TenantUserClaims);
                builder.HasData(Demo2Data.TenantUserClaims);
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>(builder =>
            {
                builder.HasData(Demo1Data.TenantUserRoles);
                builder.HasData(Demo2Data.TenantUserRoles);
            });

            #endregion

            #region Application tables

            modelBuilder.Entity<OrderProduct>()
                .HasKey(x => new {x.OrderId, x.ProductId});
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