// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using Chabloom.Ecommerce.Backend.Models;
using Chabloom.Ecommerce.Backend.Models.Authorization;

// ReSharper disable StringLiteralTypo

namespace Chabloom.Ecommerce.Backend.Data
{
    public static class Demo2Data
    {
        public static Tenant Tenant { get; } = new()
        {
            Id = Guid.Parse("9CAFCE7F-D4A1-4874-B3C9-339836FD082C"),
            Name = "Augusta Green Jackets",
            CreatedUser = Guid.Empty,
            CreatedTimestamp = DateTimeOffset.MinValue
        };

        public static List<TenantRole> TenantRoles { get; } = new()
        {
            new TenantRole
            {
                Id = Guid.Parse("6F2183CB-401C-4D7C-9C3C-ABC0E420F4F3"),
                Name = "Admin",
                TenantId = Tenant.Id,
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new TenantRole
            {
                Id = Guid.Parse("F6079515-7ED4-4BCF-B476-E747E31EBDBB"),
                Name = "Manager",
                TenantId = Tenant.Id,
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            }
        };

        public static List<ProductCategory> ProductCategories { get; } = new()
        {
            new ProductCategory
            {
                Id = Guid.Parse("1DEF1630-85EF-4A97-A073-FD3BA814BAB0"),
                Name = "Drinks",
                Description = "Drinks",
                TenantId = Tenant.Id,
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new ProductCategory
            {
                Id = Guid.Parse("F2B822FC-4E6E-4C65-A5BB-74D080C9E33A"),
                Name = "Food",
                Description = "Food",
                TenantId = Tenant.Id,
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            }
        };

        public static List<Product> Products { get; } = new()
        {
            new Product
            {
                Id = Guid.Parse("9AA49AE2-53BB-417A-B1F7-1BD9F6578969"),
                Name = "Beer",
                Description = "Beer",
                Price = 5.99M,
                CategoryId = Guid.Parse("1DEF1630-85EF-4A97-A073-FD3BA814BAB0"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new Product
            {
                Id = Guid.Parse("0AECBBC7-0E6C-4727-BC05-9D3700397B00"),
                Name = "Water",
                Description = "Water",
                Price = 4.99M,
                CategoryId = Guid.Parse("1DEF1630-85EF-4A97-A073-FD3BA814BAB0"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new Product
            {
                Id = Guid.Parse("781D9646-1156-4CBB-A581-329F2AE34744"),
                Name = "Hamburger",
                Description = "Hamburger",
                Price = 3.99M,
                CategoryId = Guid.Parse("F2B822FC-4E6E-4C65-A5BB-74D080C9E33A"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new Product
            {
                Id = Guid.Parse("C615100B-E2D9-48A4-81C1-824A3BB12CB7"),
                Name = "Hot dog",
                Description = "Hot dog",
                Price = 1.99M,
                CategoryId = Guid.Parse("F2B822FC-4E6E-4C65-A5BB-74D080C9E33A"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            }
        };

        public static List<ProductImage> ProductImages { get; } = new()
        {
            new ProductImage
            {
                Id = Guid.Parse("84519E6F-1C4E-426B-90E7-F3B444A5CE79"),
                Filename = "9AA49AE2-53BB-417A-B1F7-1BD9F6578969.webp",
                ProductId = Guid.Parse("9AA49AE2-53BB-417A-B1F7-1BD9F6578969"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new ProductImage
            {
                Id = Guid.Parse("3E8B9E9F-6A32-4262-96C7-784B8EA455E8"),
                Filename = "0AECBBC7-0E6C-4727-BC05-9D3700397B00.webp",
                ProductId = Guid.Parse("0AECBBC7-0E6C-4727-BC05-9D3700397B00"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new ProductImage
            {
                Id = Guid.Parse("75A0C5A5-B91A-4C0F-A573-9C8CFE70EE7A"),
                Filename = "781D9646-1156-4CBB-A581-329F2AE34744.webp",
                ProductId = Guid.Parse("781D9646-1156-4CBB-A581-329F2AE34744"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new ProductImage
            {
                Id = Guid.Parse("D3FDE56F-5393-4641-B573-A3E71960D542"),
                Filename = "C615100B-E2D9-48A4-81C1-824A3BB12CB7.webp",
                ProductId = Guid.Parse("C615100B-E2D9-48A4-81C1-824A3BB12CB7"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
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
    }
}