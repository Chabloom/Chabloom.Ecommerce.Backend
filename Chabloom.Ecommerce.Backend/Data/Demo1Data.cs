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
    public static class Demo1Data
    {
        public static Tenant Tenant { get; } = new()
        {
            Id = Guid.Parse("6A7E29DC-9EFF-4F0D-BB14-51F63F142871"),
            Name = "Joe's Tea Shop"
        };
        
        public static List<TenantHost> TenantHosts { get; } = new()
        {
            new TenantHost
            {
                Hostname = "tea.dev-1.chabloom.com",
                TenantId = Tenant.Id
            },
            new TenantHost
            {
                Hostname = "tea.uat-1.chabloom.com",
                TenantId = Tenant.Id
            }
        };

        public static List<TenantRole> TenantRoles { get; } = new()
        {
            new TenantRole
            {
                Id = Guid.Parse("830C7015-AB6C-4988-A603-AE3DC532D3B7"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "cd5950f5-abf4-436c-96f3-72ae9ef650d0",
                TenantId = Tenant.Id
            },
            new TenantRole
            {
                Id = Guid.Parse("30F42A18-8821-4913-B562-33D46D28F158"),
                Name = "Manager",
                NormalizedName = "MANAGER",
                ConcurrencyStamp = "4a9c08cb-d283-42f6-9fa0-face7d3606b9",
                TenantId = Tenant.Id
            }
        };

        public static List<TenantUser> TenantUsers { get; } = new()
        {
            new TenantUser
            {
                Id = Guid.Parse("DC9FF447-53EE-466D-9928-93D22D8495C0"),
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
                ConcurrencyStamp = "E069977E-0AC5-4E43-AC44-FA297AB43951",
                TenantId = Tenant.Id
            }
        };

        public static List<IdentityUserClaim<Guid>> TenantUserClaims { get; } = new()
        {
            new IdentityUserClaim<Guid>
            {
                Id = 1,
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
                Id = Guid.Parse("7B3A059D-4CDA-46CC-890E-2C1F6451D6D6"),
                Name = "Black",
                Description = "While black teas are made from the same Camellia sinensis plant as all teas, the " +
                              "oxidation and processing is what distinguishes black teas from the rest. Premium " +
                              "black teas are withered, rolled, oxidized and fired in an oven, creating a warm and " +
                              "toasty flavor. The lengthier oxidation process causes the tea leaves to develop " +
                              "into dark brown and black colors. The flavors can range from malty or smokey to " +
                              "fruity and sweet. Black teas range from mellow teas from China to full-bodied teas " +
                              "from Assam, India.",
                TenantId = Tenant.Id
            },
            new ProductCategory
            {
                Id = Guid.Parse("66272963-7577-4FB3-8CD6-A0BC411404E9"),
                Name = "Assam",
                Description =
                    "Assam is India's largest producer of tea, and the broad flood plains make for some " +
                    "of the most fertile tea estates in the world.",
                TenantId = Tenant.Id,
                ParentCategoryId = Guid.Parse("7B3A059D-4CDA-46CC-890E-2C1F6451D6D6")
            },
            new ProductCategory
            {
                Id = Guid.Parse("CF059C18-EC58-4C6E-AE61-4DDDABD61A6D"),
                Name = "Darjeeling",
                Description =
                    "The light and brisk First Flush teas focus your attention. While the darker teas of " +
                    "the later seasons are more mellow and often have wonderful flavors.",
                TenantId = Tenant.Id,
                ParentCategoryId = Guid.Parse("7B3A059D-4CDA-46CC-890E-2C1F6451D6D6")
            },
            new ProductCategory
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
                TenantId = Tenant.Id
            },
            new ProductCategory
            {
                Id = Guid.Parse("B5E4F99F-227E-49BE-A82F-8B4E06D35D96"),
                Name = "Matcha",
                Description =
                    "Powdered green teas have been consumed in China and Japan for centuries. However it " +
                    "is only in the last few decades that Westerners have acquired a taste for this " +
                    "ancient tea. We enjoy the bracing vegetal flavors, as well as the unusual process " +
                    "for preparing the tea.",
                TenantId = Tenant.Id,
                ParentCategoryId = Guid.Parse("6470CA64-4D0A-4D94-8333-0F06D74E7CA1")
            },
            new ProductCategory
            {
                Id = Guid.Parse("7D582944-4E2F-42EE-8A1E-199FD58762A6"),
                Name = "Herbal",
                Description = "Herbal teas, also known as herbal infusions, are typically a blend of herbs, " +
                              "flowers, spices, and dried fruit. This blend of ingredients is then brewed in the " +
                              "same way as your favorite traditional tea, either loose or in tea sachets or bags. " +
                              "By combining quality ingredients, blends can be created that calm, invigorate, or " +
                              "treat minor ailments. Colors and flavors range from light and fruity to vibrant and " +
                              "spicy, to match your mood.",
                TenantId = Tenant.Id
            }
        };

        public static List<Product> Products { get; } = new()
        {
            new Product
            {
                Id = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                Name = "Organic Assam (25 tea bags)",
                Description =
                    "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly " +
                    "lighter liquor, with sweet honey flavor.",
                Amount = 299,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("66272963-7577-4FB3-8CD6-A0BC411404E9")
            },
            new Product
            {
                Id = Guid.Parse("41E5E396-6757-42F3-9149-3B084976545A"),
                Name = "Organic Assam (cup)",
                Description =
                    "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly " +
                    "lighter liquor, with sweet honey flavor.",
                Amount = 299,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("66272963-7577-4FB3-8CD6-A0BC411404E9")
            },
            new Product
            {
                Id = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                Name = "Puttabong 1st Flush Darjeeling (25 tea bags)",
                Description = "Our friends at Puttabong have done a great job with this tea. It is owned by " +
                              "Jayshree and is located north of Darjeeling town.  This tea came towards the end of " +
                              "the First Flush season in April. Brisk yet flavorful. Hats off!",
                Amount = 499,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("CF059C18-EC58-4C6E-AE61-4DDDABD61A6D")
            },
            new Product
            {
                Id = Guid.Parse("4D0BCD02-9DAB-499E-92CD-8BA9F252B2A9"),
                Name = "Puttabong 1st Flush Darjeeling (cup)",
                Description = "Our friends at Puttabong have done a great job with this tea. It is owned by " +
                              "Jayshree and is located north of Darjeeling town.  This tea came towards the end of " +
                              "the First Flush season in April. Brisk yet flavorful. Hats off!",
                Amount = 499,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("CF059C18-EC58-4C6E-AE61-4DDDABD61A6D")
            },
            new Product
            {
                Id = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                Name = "Yuzu Sencha (25 tea bags)",
                Description = "It seems like it is Yuzu’s time to shine. People are really liking this citrus " +
                              "fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we " +
                              "should try it.",
                Amount = 299,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("6470CA64-4D0A-4D94-8333-0F06D74E7CA1")
            },
            new Product
            {
                Id = Guid.Parse("0418AE94-B020-4B2A-9697-7DDCBE2BD72A"),
                Name = "Yuzu Sencha (cup)",
                Description = "It seems like it is Yuzu’s time to shine. People are really liking this citrus " +
                              "fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we " +
                              "should try it.",
                Amount = 299,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("6470CA64-4D0A-4D94-8333-0F06D74E7CA1")
            },
            new Product
            {
                Id = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                Name = "Organic Matcha (25 tea bags)",
                Description = "Matcha powdered green tea has been the pride of Uji for several centuries. This " +
                              "organic grade is great for everyday use.",
                Amount = 399,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("B5E4F99F-227E-49BE-A82F-8B4E06D35D96")
            },
            new Product
            {
                Id = Guid.Parse("E95543DA-BB67-4859-8B98-D92041D58D8D"),
                Name = "Organic Matcha (cup)",
                Description = "Matcha powdered green tea has been the pride of Uji for several centuries. This " +
                              "organic grade is great for everyday use.",
                Amount = 399,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("B5E4F99F-227E-49BE-A82F-8B4E06D35D96")
            },
            new Product
            {
                Id = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                Name = "Strawberry Kiwi Fruit Tea (25 tea bags)",
                Description = "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit " +
                              "pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It " +
                              "looks festive brewed in a glass teapot, and tastes delicious hot or iced. ",
                Amount = 299,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("7D582944-4E2F-42EE-8A1E-199FD58762A6")
            },
            new Product
            {
                Id = Guid.Parse("728617AA-ECD3-48AC-BDAD-CCF660F775A3"),
                Name = "Strawberry Kiwi Fruit Tea (cup)",
                Description = "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit " +
                              "pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It " +
                              "looks festive brewed in a glass teapot, and tastes delicious hot or iced. ",
                Amount = 299,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("7D582944-4E2F-42EE-8A1E-199FD58762A6")
            },
            new Product
            {
                Id = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                Name = "Blood Orange Fruit Tea (25 tea bags)",
                Description = "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and " +
                              "distinctive twist found in blood oranges. Delicious hot or cold, it brews an " +
                              "aromatic and vivid shade of orange.",
                Amount = 299,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("7D582944-4E2F-42EE-8A1E-199FD58762A6")
            },
            new Product
            {
                Id = Guid.Parse("2446BD16-DF0A-4F7E-9E23-18CB1D5D008E"),
                Name = "Blood Orange Fruit Tea (cup)",
                Description = "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and " +
                              "distinctive twist found in blood oranges. Delicious hot or cold, it brews an " +
                              "aromatic and vivid shade of orange.",
                Amount = 299,
                CurrencyId = "USD",
                CategoryId = Guid.Parse("7D582944-4E2F-42EE-8A1E-199FD58762A6")
            }
        };

        public static List<ProductImage> ProductImages { get; } = new()
        {
            new ProductImage
            {
                Name = "323565D2-3C93-4E05-81FF-AC745E22AF9E.webp",
                ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E")
            },
            new ProductImage
            {
                Name = "323565D2-3C93-4E05-81FF-AC745E22AF9E.webp",
                ProductId = Guid.Parse("41E5E396-6757-42F3-9149-3B084976545A")
            },
            new ProductImage
            {
                Name = "78E540DE-D2B3-4B1F-BB1E-988BE3245088.webp",
                ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088")
            },
            new ProductImage
            {
                Name = "78E540DE-D2B3-4B1F-BB1E-988BE3245088.webp",
                ProductId = Guid.Parse("4D0BCD02-9DAB-499E-92CD-8BA9F252B2A9")
            },
            new ProductImage
            {
                Name = "CB949DDA-57FB-4731-8379-B6F955B3102E.webp",
                ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E")
            },
            new ProductImage
            {
                Name = "CB949DDA-57FB-4731-8379-B6F955B3102E.webp",
                ProductId = Guid.Parse("0418AE94-B020-4B2A-9697-7DDCBE2BD72A")
            },
            new ProductImage
            {
                Name = "5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE.webp",
                ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE")
            },
            new ProductImage
            {
                Name = "5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE.webp",
                ProductId = Guid.Parse("E95543DA-BB67-4859-8B98-D92041D58D8D")
            },
            new ProductImage
            {
                Name = "CE3E245B-75C5-418E-98FE-3A115AA7395D.webp",
                ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D")
            },
            new ProductImage
            {
                Name = "CE3E245B-75C5-418E-98FE-3A115AA7395D.webp",
                ProductId = Guid.Parse("728617AA-ECD3-48AC-BDAD-CCF660F775A3")
            },
            new ProductImage
            {
                Name = "0321E99E-DD3B-402F-9CF6-E2BA284862D0.webp",
                ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0")
            },
            new ProductImage
            {
                Name = "0321E99E-DD3B-402F-9CF6-E2BA284862D0.webp",
                ProductId = Guid.Parse("2446BD16-DF0A-4F7E-9E23-18CB1D5D008E")
            }
        };

        public static List<ProductPickupMethod> ProductPickupMethods { get; } = new()
        {
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                PickupMethodName = "Shipping"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("41E5E396-6757-42F3-9149-3B084976545A"),
                PickupMethodName = "In-Store"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                PickupMethodName = "Shipping"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("4D0BCD02-9DAB-499E-92CD-8BA9F252B2A9"),
                PickupMethodName = "In-Store"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                PickupMethodName = "Shipping"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("0418AE94-B020-4B2A-9697-7DDCBE2BD72A"),
                PickupMethodName = "In-Store"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                PickupMethodName = "Shipping"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("E95543DA-BB67-4859-8B98-D92041D58D8D"),
                PickupMethodName = "In-Store"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                PickupMethodName = "Shipping"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("728617AA-ECD3-48AC-BDAD-CCF660F775A3"),
                PickupMethodName = "In-Store"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                PickupMethodName = "Pickup"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                PickupMethodName = "Shipping"
            },
            new ProductPickupMethod
            {
                ProductId = Guid.Parse("2446BD16-DF0A-4F7E-9E23-18CB1D5D008E"),
                PickupMethodName = "In-Store"
            }
        };

        public static List<Store> Stores { get; } = new()
        {
            new Store
            {
                Id = Guid.Parse("87CEED1B-C17D-4A46-B71E-A1B2B3417483"),
                WarehouseId = Guid.Parse("69070B35-9ED3-47DD-A919-300371F54634"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new Store
            {
                Id = Guid.Parse("030A985E-FDF3-40D5-84DD-D88011061FB2"),
                WarehouseId = Guid.Parse("92A73ACA-281A-4CE2-9970-A1D6FBB75802"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            }
        };

        public static List<Warehouse> Warehouses { get; } = new()
        {
            new Warehouse
            {
                Id = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                Name = "Atlanta Warehouse",
                Description = "Atlanta Warehouse",
                Address = "500 Great SW Pkwy SW, Atlanta, GA 30336",
                TenantId = Guid.Parse("6A7E29DC-9EFF-4F0D-BB14-51F63F142871"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new Warehouse
            {
                Id = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                Name = "San Diego Warehouse",
                Description = "San Diego Warehouse",
                Address = "650 Gateway Center Dr, San Diego, CA 92102",
                TenantId = Guid.Parse("6A7E29DC-9EFF-4F0D-BB14-51F63F142871"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new Warehouse
            {
                Id = Guid.Parse("69070B35-9ED3-47DD-A919-300371F54634"),
                Name = "Charlotte Store",
                Description = "Charlotte Store",
                Address = "201 N Tryon St, Charlotte, NC 28202",
                TenantId = Guid.Parse("6A7E29DC-9EFF-4F0D-BB14-51F63F142871"),
                CreatedUser = Guid.Empty,
                CreatedTimestamp = DateTimeOffset.MinValue
            },
            new Warehouse
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

        public static List<WarehouseProduct> WarehouseProducts { get; } = new()
        {
            new WarehouseProduct
            {
                //Id = Guid.Parse("BCE64F3A-B012-48D3-89F2-36B84D5F133C"),
                WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                Count = 2131
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("A43482C7-9549-4F5C-816F-2A6BA40D5115"),
                WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                Count = 0
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("736D93A3-4F54-416A-AB0D-3B2BD8F7C45D"),
                WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                Count = 9753
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("1ADB29FD-A90D-4C00-86AF-038AB3E0E024"),
                WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                Count = 1239
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("41533622-4FB8-4C5B-889A-459681765AAF"),
                WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                Count = 1327
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("6E4EA917-5E80-414E-BC57-0A47F4A6CB8D"),
                WarehouseId = Guid.Parse("95D98C98-8C88-4A15-B3AE-9DDB9B10848B"),
                ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                Count = 1237
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("ACA91FC8-D5AA-4376-9921-F12917434D33"),
                WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                ProductId = Guid.Parse("323565D2-3C93-4E05-81FF-AC745E22AF9E"),
                Count = 7865
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("FB007436-3430-4DCF-BBFF-075040EBD8ED"),
                WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                ProductId = Guid.Parse("78E540DE-D2B3-4B1F-BB1E-988BE3245088"),
                Count = 0
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("788B957B-033C-455B-9EC7-E9F866F022FE"),
                WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                ProductId = Guid.Parse("CB949DDA-57FB-4731-8379-B6F955B3102E"),
                Count = 1231
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("DC710497-C38A-49F1-BBB6-91FB58F7630D"),
                WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                ProductId = Guid.Parse("5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE"),
                Count = 6655
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("291F2B10-43C4-48F8-8984-AE77C6A15021"),
                WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                ProductId = Guid.Parse("CE3E245B-75C5-418E-98FE-3A115AA7395D"),
                Count = 1235
            },
            new WarehouseProduct
            {
                //Id = Guid.Parse("D3F3AECE-660E-482F-A24E-544EA07B516A"),
                WarehouseId = Guid.Parse("68A72052-18F4-4E2A-A165-C057F61F86B5"),
                ProductId = Guid.Parse("0321E99E-DD3B-402F-9CF6-E2BA284862D0"),
                Count = 2313
            }
        };
    }
}