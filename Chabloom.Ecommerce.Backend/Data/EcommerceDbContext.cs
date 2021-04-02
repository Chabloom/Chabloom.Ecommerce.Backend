// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using Chabloom.Ecommerce.Backend.Models;
using Chabloom.Ecommerce.Backend.Models.Authorization;
using Chabloom.Ecommerce.Backend.Models.Inventory;
using Microsoft.EntityFrameworkCore;

// ReSharper disable StringLiteralTypo

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

            // Set up default roles
            var roles = new List<Role>
            {
                new()
                {
                    Id = Guid.Parse("D4DC0126-C55D-474E-A44B-7E6D90822A59"),
                    Name = "Admin",
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("5AB67841-F85C-416F-8A81-81B85BB7B219"),
                    Name = "Manager",
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                }
            };

            modelBuilder.Entity<Role>()
                .HasData(roles);

            // Set up demo tenant
            var demoTenant = new Tenant
            {
                Id = Guid.Parse("6A7E29DC-9EFF-4F0D-BB14-51F63F142871"),
                Name = "Joe's Tea Shop",
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            };

            modelBuilder.Entity<Tenant>()
                .HasData(demoTenant);

            // Set up demo tenant roles
            var demoTenantRoles = new List<TenantRole>
            {
                new()
                {
                    Id = Guid.Parse("830C7015-AB6C-4988-A603-AE3DC532D3B7"),
                    Name = "Admin",
                    TenantId = demoTenant.Id,
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("30F42A18-8821-4913-B562-33D46D28F158"),
                    Name = "Manager",
                    TenantId = demoTenant.Id,
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                }
            };

            modelBuilder.Entity<TenantRole>()
                .HasData(demoTenantRoles);

            // Set up demo product categories
            var demoProductCategories = new List<ProductCategory>
            {
                new()
                {
                    Id = Guid.Parse("7B3A059D-4CDA-46CC-890E-2C1F6451D6D6"),
                    Name = "Black",
                    Description = "While black teas are made from the same Camellia sinensis plant as all teas, the " +
                                  "oxidation and processing is what distinguishes black teas from the rest. Premium " +
                                  "black teas are withered, rolled, oxidized and fired in an oven, creating a warm and " +
                                  "toasty flavor. The lengthier oxidation process causes the tea leaves to develop " +
                                  "into dark brown and black colors. The flavors can range from malty or smokey to " +
                                  "fruity and sweet. Black teas range from mellow teas from China to full-bodied teas " +
                                  "from Assam, India.",
                    TenantId = demoTenant.Id,
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("66272963-7577-4FB3-8CD6-A0BC411404E9"),
                    Name = "Assam",
                    Description =
                        "Assam is India's largest producer of tea, and the broad flood plains make for some " +
                        "of the most fertile tea estates in the world.",
                    TenantId = demoTenant.Id,
                    ParentCategoryId = Guid.Parse("7B3A059D-4CDA-46CC-890E-2C1F6451D6D6"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("CF059C18-EC58-4C6E-AE61-4DDDABD61A6D"),
                    Name = "Darjeeling",
                    Description =
                        "The light and brisk First Flush teas focus your attention. While the darker teas of " +
                        "the later seasons are more mellow and often have wonderful flavors.",
                    TenantId = demoTenant.Id,
                    ParentCategoryId = Guid.Parse("7B3A059D-4CDA-46CC-890E-2C1F6451D6D6"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("6470CA64-4D0A-4D94-8333-0F06D74E7CA1"),
                    Name = "Green",
                    Description =
                        "All green teas originate from the same species, the Camelia Sinensis. To make green " +
                        "tea, the fresh tea leaves are briefly cooked using either steam or dry heat. This " +
                        "process fixes the green colors and fresh flavors. The Chinese green teas are more " +
                        "mellow and smooth, while the Japanese green teas have the heft of rich, vegetal " +
                        "flavors, which comes from preservation of the chlorophyll.The general rule is that " +
                        "a cup of green tea contains about one-third as much caffeine as a cup of coffee. " +
                        "Green tea production methods vary but the focus is always to fix the green color. " +
                        "Thus, green teas are not oxidized.",
                    TenantId = demoTenant.Id,
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("B5E4F99F-227E-49BE-A82F-8B4E06D35D96"),
                    Name = "Matcha",
                    Description =
                        "Powdered green teas have been consumed in China and Japan for centuries. However it " +
                        "is only in the last few decades that Westerners have acquired a taste for this " +
                        "ancient tea. We enjoy the bracing vegetal flavors, as well as the unusual process " +
                        "for preparing the tea.",
                    TenantId = demoTenant.Id,
                    ParentCategoryId = Guid.Parse("6470CA64-4D0A-4D94-8333-0F06D74E7CA1"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("7D582944-4E2F-42EE-8A1E-199FD58762A6"),
                    Name = "Herbal",
                    Description = "Herbal teas, also known as herbal infusions, are typically a blend of herbs, " +
                                  "flowers, spices, and dried fruit. This blend of ingredients is then brewed in the " +
                                  "same way as your favorite traditional tea, either loose or in tea sachets or bags. " +
                                  "By combining quality ingredients, blends can be created that calm, invigorate, or " +
                                  "treat minor ailments. Colors and flavors range from light and fruity to vibrant and " +
                                  "spicy, to match your mood.",
                    TenantId = demoTenant.Id,
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                }
            };

            modelBuilder.Entity<ProductCategory>()
                .HasData(demoProductCategories);

            // Set up demo products
            var demoProducts = new List<Product>
            {
                new()
                {
                    Id = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                    Name = "Organic Assam",
                    Description =
                        "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly " +
                        "lighter liquor, with sweet honey flavor.",
                    Price = 2.99M,
                    CategoryId = Guid.Parse("66272963-7577-4FB3-8CD6-A0BC411404E9"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                    Name = "Puttabong 1st Flush Darjeeling",
                    Description = "Our friends at Puttabong have done a great job with this tea. It is owned by " +
                                  "Jayshree and is located north of Darjeeling town.  This tea came towards the end of " +
                                  "the First Flush season in April. Brisk yet flavorful. Hats off!",
                    Price = 4.99M,
                    CategoryId = Guid.Parse("CF059C18-EC58-4C6E-AE61-4DDDABD61A6D"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                    Name = "Yuzu Sencha",
                    Description = "It seems like it is Yuzu’s time to shine. People are really liking this citrus " +
                                  "fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we " +
                                  "should try it.",
                    Price = 2.99M,
                    CategoryId = Guid.Parse("6470CA64-4D0A-4D94-8333-0F06D74E7CA1"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                    Name = "Organic Matcha",
                    Description = "Matcha powdered green tea has been the pride of Uji for several centuries. This " +
                                  "organic grade is great for everyday use.",
                    Price = 3.99M,
                    CategoryId = Guid.Parse("B5E4F99F-227E-49BE-A82F-8B4E06D35D96"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                    Name = "Strawberry Kiwi Fruit Tea",
                    Description = "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit " +
                                  "pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It " +
                                  "looks festive brewed in a glass teapot, and tastes delicious hot or iced. ",
                    Price = 2.99M,
                    CategoryId = Guid.Parse("7D582944-4E2F-42EE-8A1E-199FD58762A6"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                    Name = "Blood Orange Fruit Tea",
                    Description = "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and " +
                                  "distinctive twist found in blood oranges. Delicious hot or cold, it brews an " +
                                  "aromatic and vivid shade of orange.",
                    Price = 2.99M,
                    CategoryId = Guid.Parse("7D582944-4E2F-42EE-8A1E-199FD58762A6"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                }
            };

            modelBuilder.Entity<Product>()
                .HasData(demoProducts);

            // Set up demo product images
            var demoProductImages = new List<ProductImage>
            {
                new()
                {
                    Id = Guid.Parse("DE6CAC85-5C34-4EA6-B1D5-B1C8EC111070"),
                    ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("9A467CBC-8128-44BE-ADDD-B85F65F57CAD"),
                    ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("5B605894-7618-4B97-A49B-9202F6E2799A"),
                    ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("622D3D2C-1FF2-43B8-94E8-36AE7C2CA86B"),
                    ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("745B7C71-03AF-4ADA-B18C-E370E5305E64"),
                    ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("6F054D9A-F2B9-49B0-86FA-71F328DD2DAF"),
                    ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                }
            };

            modelBuilder.Entity<ProductImage>()
                .HasData(demoProductImages);

            // Set up demo stores
            var demoStores = new List<Store>
            {
                new()
                {
                    Id = Guid.Parse("69070B35-9ED3-47DD-A919-300371F54634"),
                    Name = "Charlotte Store",
                    Description = "Charlotte Store",
                    Address = "201 N Tryon St, Charlotte, NC 28202",
                    TenantId = Guid.Parse("6A7E29DC-9EFF-4F0D-BB14-51F63F142871"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("92A73ACA-281A-4CE2-9970-A1D6FBB75802"),
                    Name = "San Fransisco Store",
                    Description = "San Fransisco Store",
                    Address = "199 Gough St, San Francisco, CA 94102",
                    TenantId = Guid.Parse("6A7E29DC-9EFF-4F0D-BB14-51F63F142871"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                }
            };

            modelBuilder.Entity<Store>()
                .HasData(demoStores);

            // Set up demo store products
            var demoStoreProducts = new List<StoreProduct>
            {
                new()
                {
                    StoreId = Guid.Parse("69070B35-9ED3-47DD-A919-300371F54634"),
                    ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                    Count = 89
                },
                new()
                {
                    StoreId = Guid.Parse("69070B35-9ED3-47DD-A919-300371F54634"),
                    ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                    Count = 78
                },
                new()
                {
                    StoreId = Guid.Parse("69070B35-9ED3-47DD-A919-300371F54634"),
                    ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                    Count = 0
                },
                new()
                {
                    StoreId = Guid.Parse("69070B35-9ED3-47DD-A919-300371F54634"),
                    ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                    Count = 11
                },
                new()
                {
                    StoreId = Guid.Parse("69070B35-9ED3-47DD-A919-300371F54634"),
                    ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                    Count = 55
                },
                new()
                {
                    StoreId = Guid.Parse("69070B35-9ED3-47DD-A919-300371F54634"),
                    ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                    Count = 123
                },
                new()
                {
                    StoreId = Guid.Parse("92A73ACA-281A-4CE2-9970-A1D6FBB75802"),
                    ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                    Count = 66
                },
                new()
                {
                    StoreId = Guid.Parse("92A73ACA-281A-4CE2-9970-A1D6FBB75802"),
                    ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                    Count = 15
                },
                new()
                {
                    StoreId = Guid.Parse("92A73ACA-281A-4CE2-9970-A1D6FBB75802"),
                    ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                    Count = 22
                },
                new()
                {
                    StoreId = Guid.Parse("92A73ACA-281A-4CE2-9970-A1D6FBB75802"),
                    ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                    Count = 512
                },
                new()
                {
                    StoreId = Guid.Parse("92A73ACA-281A-4CE2-9970-A1D6FBB75802"),
                    ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                    Count = 33
                },
                new()
                {
                    StoreId = Guid.Parse("92A73ACA-281A-4CE2-9970-A1D6FBB75802"),
                    ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                    Count = 0
                }
            };

            modelBuilder.Entity<StoreProduct>()
                .HasData(demoStoreProducts);

            // Set up demo warehouses
            var demoWarehouses = new List<Warehouse>
            {
                new()
                {
                    Id = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                    Name = "Atlanta Warehouse",
                    Description = "Atlanta Warehouse",
                    Address = "500 Great SW Pkwy SW, Atlanta, GA 30336",
                    TenantId = Guid.Parse("6A7E29DC-9EFF-4F0D-BB14-51F63F142871"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                },
                new()
                {
                    Id = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                    Name = "San Diego Warehouse",
                    Description = "San Diego Warehouse",
                    Address = "650 Gateway Center Dr, San Diego, CA 92102",
                    TenantId = Guid.Parse("6A7E29DC-9EFF-4F0D-BB14-51F63F142871"),
                    CreatedUser = Guid.Empty,
                    CreatedTimestamp = DateTimeOffset.MinValue
                }
            };

            modelBuilder.Entity<Warehouse>()
                .HasData(demoWarehouses);

            // Set up demo warehouse products
            var demoWarehouseProducts = new List<WarehouseProduct>
            {
                new()
                {
                    WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                    ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                    Count = 2131
                },
                new()
                {
                    WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                    ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                    Count = 0
                },
                new()
                {
                    WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                    ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                    Count = 9753
                },
                new()
                {
                    WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                    ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                    Count = 1239
                },
                new()
                {
                    WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                    ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                    Count = 1327
                },
                new()
                {
                    WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                    ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                    Count = 1237
                },
                new()
                {
                    WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                    ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                    Count = 7865
                },
                new()
                {
                    WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                    ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                    Count = 0
                },
                new()
                {
                    WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                    ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                    Count = 1231
                },
                new()
                {
                    WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                    ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                    Count = 6655
                },
                new()
                {
                    WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                    ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                    Count = 1235
                },
                new()
                {
                    WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                    ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                    Count = 2313
                }
            };

            modelBuilder.Entity<WarehouseProduct>()
                .HasData(demoWarehouseProducts);
        }
    }
}