// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using Chabloom.Ecommerce.Backend.Models.Products;
using Chabloom.Ecommerce.Backend.Models.Stores;
using Chabloom.Ecommerce.Backend.Models.Tenants;
using Chabloom.Ecommerce.Backend.Models.Warehouses;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

// ReSharper disable StringLiteralTypo

namespace Chabloom.Ecommerce.Backend.Data
{
    public static class Demo2Data
    {
        public static Tenant Tenant { get; } = new()
        {
            Id = Guid.Parse("9CAFCE7F-D4A1-4874-B3C9-339836FD082C"),
            Name = "Augusta Green Jackets"
        };

        public static List<TenantHost> TenantHosts { get; } = new()
        {
            new TenantHost
            {
                Hostname = "ecommerce-dev-1.chabloom.com",
                TenantId = Tenant.Id
            },
            new TenantHost
            {
                Hostname = "ecommerce-uat-1.chabloom.com",
                TenantId = Tenant.Id
            },
            new TenantHost
            {
                Hostname = "greenjackets.dev-1.chabloom.com",
                TenantId = Tenant.Id
            },
            new TenantHost
            {
                Hostname = "greenjackets.uat-1.chabloom.com",
                TenantId = Tenant.Id
            }
        };

        public static List<TenantRole> TenantRoles { get; } = new()
        {
            new TenantRole
            {
                Id = Guid.Parse("6F2183CB-401C-4D7C-9C3C-ABC0E420F4F3"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "ffb84d0f-9767-47ee-9e6a-0681709b8297",
                TenantId = Tenant.Id
            },
            new TenantRole
            {
                Id = Guid.Parse("F6079515-7ED4-4BCF-B476-E747E31EBDBB"),
                Name = "Manager",
                NormalizedName = "MANAGER",
                ConcurrencyStamp = "4e6eff0b-c1a8-4f89-b90f-397b47ee57d4",
                TenantId = Tenant.Id
            }
        };

        public static List<TenantUser> TenantUsers { get; } = new()
        {
            new TenantUser
            {
                Id = Guid.Parse("DCD60458-2C66-4902-9861-4E6A3075A60C"),
                UserName = "mdcasey@chabloom.com",
                NormalizedUserName = "MDCASEY@CHABLOOM.COM",
                Email = "mdcasey@chabloom.com",
                NormalizedEmail = "MDCASEY@CHABLOOM.COM",
                EmailConfirmed = true,
                PhoneNumber = "+18036179564",
                PhoneNumberConfirmed = true,
                PasswordHash =
                    "AQAAAAEAACcQAAAAELYyWQtU3cVbIfdmk4LHrtYsKTiYVW7OAge27lolZ3I8D97OE4QQ6Yn4XwGhO8YPuQ==",
                SecurityStamp = "C3KZM3I2WQCCAD7EVHRZQSGRFRX5MY3I",
                ConcurrencyStamp = "7265D536-0D5F-49EE-AE87-81C14D3D0BE1",
                TenantId = Tenant.Id
            }
        };

        public static List<IdentityUserClaim<Guid>> TenantUserClaims { get; } = new()
        {
            new IdentityUserClaim<Guid>
            {
                Id = 2,
                UserId = TenantUsers[0].Id,
                ClaimType = JwtClaimTypes.Name,
                ClaimValue = "Matthew Casey"
            }
        };

        public static List<IdentityUserRole<Guid>> TenantUserRoles { get; } = new()
        {
            new IdentityUserRole<Guid>
            {
                UserId = TenantUsers[0].Id,
                RoleId = TenantRoles[0].Id
            },
        };

        public static List<ProductCategory> ProductCategories { get; } = new()
        {
            new ProductCategory
            {
                Id = Guid.Parse("1DEF1630-85EF-4A97-A073-FD3BA814BAB0"),
                Name = "Drinks",
                Description = "Drinks",
                TenantId = Tenant.Id
            },
            new ProductCategory
            {
                Id = Guid.Parse("F2B822FC-4E6E-4C65-A5BB-74D080C9E33A"),
                Name = "Food",
                Description = "Food",
                TenantId = Tenant.Id
            }
        };

        public static List<Product> Products { get; } = new()
        {
            new Product
            {
                Id = Guid.Parse("9AA49AE2-53BB-417A-B1F7-1BD9F6578969"),
                Name = "Beer",
                Description =
                    "Bud Light is a premium beer with incredible drinkability that has made it a top selling " +
                    "American beer that everybody knows and loves. This light beer is brewed using a " +
                    "combination of barley malts, rice and a blend of premium aroma hop varieties. Featuring " +
                    "a fresh, clean taste with subtle hop aromas, this light lager delivers ultimate " +
                    "refreshment with its delicate malt sweetness and crisp finish.",
                Amount = 599,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("1DEF1630-85EF-4A97-A073-FD3BA814BAB0")
            },
            new Product
            {
                Id = Guid.Parse("0AECBBC7-0E6C-4727-BC05-9D3700397B00"),
                Name = "Water",
                Description = "Designed to be a great tasting water, our water is filtered by reverse osmosis to " +
                              "remove impurities, then enhanced with a special blend of minerals for a pure, crisp, " +
                              "fresh taste.",
                Amount = 499,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("1DEF1630-85EF-4A97-A073-FD3BA814BAB0")
            },
            new Product
            {
                Id = Guid.Parse("781D9646-1156-4CBB-A581-329F2AE34744"),
                Name = "Hamburger",
                Description = "The original burger starts with a 100% pure beef burger seasoned with just a pinch of " +
                              "salt and pepper. Then, the burger is topped with a tangy pickle, chopped onions, " +
                              "ketchup and mustard.",
                Amount = 399,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("F2B822FC-4E6E-4C65-A5BB-74D080C9E33A")
            },
            new Product
            {
                Id = Guid.Parse("C615100B-E2D9-48A4-81C1-824A3BB12CB7"),
                Name = "Hot dog",
                Description = "Our tasty all beef hot dogs are all natural, skinless, uncured and made with beef " +
                              "that’s never given antibiotics.",
                Amount = 199,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("F2B822FC-4E6E-4C65-A5BB-74D080C9E33A")
            }
        };

        public static List<ProductImage> ProductImages { get; } = new()
        {
            new ProductImage
            {
                Name = "9AA49AE2-53BB-417A-B1F7-1BD9F6578969.webp",
                ProductId = Guid.Parse("9AA49AE2-53BB-417A-B1F7-1BD9F6578969")
            },
            new ProductImage
            {
                Name = "0AECBBC7-0E6C-4727-BC05-9D3700397B00.webp",
                ProductId = Guid.Parse("0AECBBC7-0E6C-4727-BC05-9D3700397B00")
            },
            new ProductImage
            {
                Name = "781D9646-1156-4CBB-A581-329F2AE34744.webp",
                ProductId = Guid.Parse("781D9646-1156-4CBB-A581-329F2AE34744")
            },
            new ProductImage
            {
                Name = "C615100B-E2D9-48A4-81C1-824A3BB12CB7.webp",
                ProductId = Guid.Parse("C615100B-E2D9-48A4-81C1-824A3BB12CB7")
            }
        };

        public static List<ProductPickupMethod> ProductPickupMethods { get; } = new()
        {
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("9AA49AE2-53BB-417A-B1F7-1BD9F6578969"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("9AA49AE2-53BB-417A-B1F7-1BD9F6578969"),
                PickupMethodName = "In-Store"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("0AECBBC7-0E6C-4727-BC05-9D3700397B00"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("0AECBBC7-0E6C-4727-BC05-9D3700397B00"),
                PickupMethodName = "In-Store"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("781D9646-1156-4CBB-A581-329F2AE34744"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("781D9646-1156-4CBB-A581-329F2AE34744"),
                PickupMethodName = "In-Store"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("C615100B-E2D9-48A4-81C1-824A3BB12CB7"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("C615100B-E2D9-48A4-81C1-824A3BB12CB7"),
                PickupMethodName = "In-Store"
            }
        };

        public static List<Store> Stores { get; } = new()
        {
            new Store
            {
                Id = Guid.Parse("9819E16B-BE09-4B40-BE9B-5E0D8B33280C"),
                WarehouseId = Guid.Parse("2AE5474A-4702-4BD0-B12E-90BC644EE223"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            }
        };

        public static List<Warehouse> Warehouses { get; } = new()
        {
            new Warehouse
            {
                Id = Guid.Parse("2AE5474A-4702-4BD0-B12E-90BC644EE223"),
                Name = "SRP Park",
                Description = "SRP Park",
                Address = "187 Railroad Ave, North Augusta, SC 29841",
                TenantId = Guid.Parse("9CAFCE7F-D4A1-4874-B3C9-339836FD082C"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            }
        };
    }
}