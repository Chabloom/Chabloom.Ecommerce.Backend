using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.Application
{
    public partial class ApplicationMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PickupMethods",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupMethods", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantHosts",
                columns: table => new
                {
                    Hostname = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantHosts", x => x.Hostname);
                    table.ForeignKey(
                        name: "FK_TenantHosts_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantRoles", x => x.Id);
                    table.UniqueConstraint("AK_TenantRoles_NormalizedName_TenantId", x => new { x.NormalizedName, x.TenantId });
                    table.ForeignKey(
                        name: "FK_TenantRoles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUsers", x => x.Id);
                    table.UniqueConstraint("AK_TenantUsers_NormalizedUserName_TenantId", x => new { x.NormalizedUserName, x.TenantId });
                    table.ForeignKey(
                        name: "FK_TenantUsers_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DisabledUser = table.Column<Guid>(type: "uuid", nullable: true),
                    DisabledTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CurrencyId = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantRoleClaims_TenantRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TenantRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantUserClaims_TenantUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "TenantUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_TenantUserLogins_TenantUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "TenantUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TenantUserRoles_TenantRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TenantRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantUserRoles_TenantUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "TenantUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TenantUserTokens_TenantUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "TenantUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DisabledUser = table.Column<Guid>(type: "uuid", nullable: true),
                    DisabledTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => new { x.Name, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPickupMethods",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    PickupMethodName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPickupMethods", x => new { x.ProductId, x.PickupMethodName });
                    table.ForeignKey(
                        name: "FK_ProductPickupMethods_PickupMethods_PickupMethodName",
                        column: x => x.PickupMethodName,
                        principalTable: "PickupMethods",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPickupMethods_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseProducts",
                columns: table => new
                {
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseProducts", x => new { x.WarehouseId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseProducts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentId = table.Column<string>(type: "text", nullable: false),
                    PickupMethodName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DisabledUser = table.Column<Guid>(type: "uuid", nullable: true),
                    DisabledTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_PickupMethods_PickupMethodName",
                        column: x => x.PickupMethodName,
                        principalTable: "PickupMethods",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CurrencyId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PickupMethods",
                column: "Name",
                values: new object[]
                {
                    "In-Store",
                    "Pickup",
                    "Delivery",
                    "Shipping"
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), "Joe's Tea Shop" },
                    { new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), "Augusta Green Jackets" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId", "TenantId" },
                values: new object[,]
                {
                    { new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), "While black teas are made from the same Camellia sinensis plant as all teas, the oxidation and processing is what distinguishes black teas from the rest. Premium black teas are withered, rolled, oxidized and fired in an oven, creating a warm and toasty flavor. The lengthier oxidation process causes the tea leaves to develop into dark brown and black colors. The flavors can range from malty or smokey to fruity and sweet. Black teas range from mellow teas from China to full-bodied teas from Assam, India.", "Black", null, new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") },
                    { new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), "All green teas originate from the same species, the Camelia Sinensis. To make green tea, the fresh tea leaves are briefly cooked using either steam or dry heat. This process fixes the green colors and fresh flavors. The Chinese green teas are more mellow and smooth, while the Japanese green teas have the heft of rich, vegetal flavors, which comes from preservation of the chlorophyll.The general rule is that a cup of green tea contains about one-third as much caffeine as a cup of coffee. Green tea production methods vary but the focus is always to fix the green color. Thus, green teas are not oxidized.", "Green", null, new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") },
                    { new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), "Herbal teas, also known as herbal infusions, are typically a blend of herbs, flowers, spices, and dried fruit. This blend of ingredients is then brewed in the same way as your favorite traditional tea, either loose or in tea sachets or bags. By combining quality ingredients, blends can be created that calm, invigorate, or treat minor ailments. Colors and flavors range from light and fruity to vibrant and spicy, to match your mood.", "Herbal", null, new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") },
                    { new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), "Drinks", "Drinks", null, new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") },
                    { new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), "Food", "Food", null, new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") }
                });

            migrationBuilder.InsertData(
                table: "TenantRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "TenantId" },
                values: new object[,]
                {
                    { new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"), "0c8bb047-f0a6-4523-8110-fd4fed52b4ae", "Admin", "ADMIN", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") },
                    { new Guid("30f42a18-8821-4913-b562-33d46d28f158"), "4970a2ee-0273-4dc6-9775-0bd0b03bae29", "Manager", "MANAGER", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") },
                    { new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"), "96309c79-49c7-470a-8c25-749cec10e97d", "Admin", "ADMIN", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") },
                    { new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"), "1000c177-6c40-4dff-a853-c36332b89186", "Manager", "MANAGER", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") }
                });

            migrationBuilder.InsertData(
                table: "TenantUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("dc9ff447-53ee-466d-9928-93d22d8495c0"), 0, "E069977E-0AC5-4E43-AC44-FA297AB43951", "mdcasey@chabloom.com", true, false, null, "MDCASEY@CHABLOOM.COM", "MDCASEY@CHABLOOM.COM", "AQAAAAEAACcQAAAAELYyWQtU3cVbIfdmk4LHrtYsKTiYVW7OAge27lolZ3I8D97OE4QQ6Yn4XwGhO8YPuQ==", "+18036179564", true, "C3KZM3I2WQCCAD7EVHRZQSGRFRX5MY3I", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), false, "mdcasey@chabloom.com" },
                    { new Guid("dcd60458-2c66-4902-9861-4e6a3075a60c"), 0, "7265D536-0D5F-49EE-AE87-81C14D3D0BE1", "mdcasey@chabloom.com", true, false, null, "MDCASEY@CHABLOOM.COM", "MDCASEY@CHABLOOM.COM", "AQAAAAEAACcQAAAAELYyWQtU3cVbIfdmk4LHrtYsKTiYVW7OAge27lolZ3I8D97OE4QQ6Yn4XwGhO8YPuQ==", "+18036179564", true, "C3KZM3I2WQCCAD7EVHRZQSGRFRX5MY3I", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), false, "mdcasey@chabloom.com" }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Address", "CreatedTimestamp", "CreatedUser", "Description", "DisabledTimestamp", "DisabledUser", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), "500 Great SW Pkwy SW, Atlanta, GA 30336", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Atlanta Warehouse", null, null, "Atlanta Warehouse", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), null, null },
                    { new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), "650 Gateway Center Dr, San Diego, CA 92102", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "San Diego Warehouse", null, null, "San Diego Warehouse", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), null, null },
                    { new Guid("69070b35-9ed3-47dd-a919-300371f54634"), "201 N Tryon St, Charlotte, NC 28202", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Charlotte Store", null, null, "Charlotte Store", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), null, null },
                    { new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), "199 Gough St, San Francisco, CA 94102", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "San Fransisco Store", null, null, "San Fransisco Store", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), null, null },
                    { new Guid("2ae5474a-4702-4bd0-b12e-90bc644ee223"), "187 Railroad Ave, North Augusta, SC 29841", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "SRP Park", null, null, "SRP Park", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), null, null }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId", "TenantId" },
                values: new object[,]
                {
                    { new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), "Assam is India's largest producer of tea, and the broad flood plains make for some of the most fertile tea estates in the world.", "Assam", new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") },
                    { new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), "The light and brisk First Flush teas focus your attention. While the darker teas of the later seasons are more mellow and often have wonderful flavors.", "Darjeeling", new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") },
                    { new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), "Powdered green teas have been consumed in China and Japan for centuries. However it is only in the last few decades that Westerners have acquired a taste for this ancient tea. We enjoy the bracing vegetal flavors, as well as the unusual process for preparing the tea.", "Matcha", new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "CategoryId", "CurrencyId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), 299m, new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), "USD", "It seems like it is Yuzu’s time to shine. People are really liking this citrus fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we should try it.", "Yuzu Sencha (25 tea bags)" },
                    { new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a"), 299m, new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), "USD", "It seems like it is Yuzu’s time to shine. People are really liking this citrus fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we should try it.", "Yuzu Sencha (cup)" },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), 299m, new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), "USD", "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It looks festive brewed in a glass teapot, and tastes delicious hot or iced. ", "Strawberry Kiwi Fruit Tea (25 tea bags)" },
                    { new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3"), 299m, new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), "USD", "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It looks festive brewed in a glass teapot, and tastes delicious hot or iced. ", "Strawberry Kiwi Fruit Tea (cup)" },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), 299m, new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), "USD", "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and distinctive twist found in blood oranges. Delicious hot or cold, it brews an aromatic and vivid shade of orange.", "Blood Orange Fruit Tea (25 tea bags)" },
                    { new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e"), 299m, new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), "USD", "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and distinctive twist found in blood oranges. Delicious hot or cold, it brews an aromatic and vivid shade of orange.", "Blood Orange Fruit Tea (cup)" },
                    { new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7"), 199m, new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), "USD", "Our tasty all beef hot dogs are all natural, skinless, uncured and made with beef that’s never given antibiotics.", "Hot dog" },
                    { new Guid("781d9646-1156-4cbb-a581-329f2ae34744"), 399m, new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), "USD", "The original burger starts with a 100% pure beef burger seasoned with just a pinch of salt and pepper. Then, the burger is topped with a tangy pickle, chopped onions, ketchup and mustard.", "Hamburger" },
                    { new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00"), 499m, new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), "USD", "Designed to be a great tasting water, our water is filtered by reverse osmosis to remove impurities, then enhanced with a special blend of minerals for a pure, crisp, fresh taste.", "Water" },
                    { new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969"), 599m, new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), "USD", "Bud Light is a premium beer with incredible drinkability that has made it a top selling American beer that everybody knows and loves. This light beer is brewed using a combination of barley malts, rice and a blend of premium aroma hop varieties. Featuring a fresh, clean taste with subtle hop aromas, this light lager delivers ultimate refreshment with its delicate malt sweetness and crisp finish.", "Beer" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "DisabledTimestamp", "DisabledUser", "UpdatedTimestamp", "UpdatedUser", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("9819e16b-be09-4b40-be9b-5e0d8b33280c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, null, new Guid("2ae5474a-4702-4bd0-b12e-90bc644ee223") },
                    { new Guid("87ceed1b-c17d-4a46-b71e-a1b2b3417483"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, null, new Guid("69070b35-9ed3-47dd-a919-300371f54634") },
                    { new Guid("030a985e-fdf3-40d5-84dd-d88011061fb2"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, null, null, null, new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802") }
                });

            migrationBuilder.InsertData(
                table: "TenantUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 2, "name", "Matthew Casey", new Guid("dcd60458-2c66-4902-9861-4e6a3075a60c") },
                    { 1, "name", "Matthew Casey", new Guid("dc9ff447-53ee-466d-9928-93d22d8495c0") }
                });

            migrationBuilder.InsertData(
                table: "TenantUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"), new Guid("dc9ff447-53ee-466d-9928-93d22d8495c0") },
                    { new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"), new Guid("dcd60458-2c66-4902-9861-4e6a3075a60c") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Name", "ProductId" },
                values: new object[,]
                {
                    { "CB949DDA-57FB-4731-8379-B6F955B3102E.webp", new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a") },
                    { "0321E99E-DD3B-402F-9CF6-E2BA284862D0.webp", new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e") },
                    { "CE3E245B-75C5-418E-98FE-3A115AA7395D.webp", new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d") },
                    { "9AA49AE2-53BB-417A-B1F7-1BD9F6578969.webp", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969") },
                    { "CE3E245B-75C5-418E-98FE-3A115AA7395D.webp", new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3") },
                    { "0AECBBC7-0E6C-4727-BC05-9D3700397B00.webp", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00") },
                    { "0321E99E-DD3B-402F-9CF6-E2BA284862D0.webp", new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0") },
                    { "781D9646-1156-4CBB-A581-329F2AE34744.webp", new Guid("781d9646-1156-4cbb-a581-329f2ae34744") },
                    { "CB949DDA-57FB-4731-8379-B6F955B3102E.webp", new Guid("cb949dda-57fb-4731-8379-b6f955b3102e") },
                    { "C615100B-E2D9-48A4-81C1-824A3BB12CB7.webp", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7") }
                });

            migrationBuilder.InsertData(
                table: "ProductPickupMethods",
                columns: new[] { "PickupMethodName", "ProductId" },
                values: new object[,]
                {
                    { "Pickup", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00") },
                    { "In-Store", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969") },
                    { "Pickup", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969") },
                    { "Pickup", new Guid("781d9646-1156-4cbb-a581-329f2ae34744") },
                    { "In-Store", new Guid("781d9646-1156-4cbb-a581-329f2ae34744") },
                    { "In-Store", new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e") },
                    { "Shipping", new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0") },
                    { "Pickup", new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0") },
                    { "In-Store", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00") },
                    { "In-Store", new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3") },
                    { "In-Store", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7") },
                    { "In-Store", new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a") },
                    { "Shipping", new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d") },
                    { "Pickup", new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d") },
                    { "Pickup", new Guid("cb949dda-57fb-4731-8379-b6f955b3102e") },
                    { "Shipping", new Guid("cb949dda-57fb-4731-8379-b6f955b3102e") },
                    { "Pickup", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "CategoryId", "CurrencyId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("41e5e396-6757-42f3-9149-3b084976545a"), 299m, new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), "USD", "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly lighter liquor, with sweet honey flavor.", "Organic Assam (cup)" },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), 499m, new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), "USD", "Our friends at Puttabong have done a great job with this tea. It is owned by Jayshree and is located north of Darjeeling town.  This tea came towards the end of the First Flush season in April. Brisk yet flavorful. Hats off!", "Puttabong 1st Flush Darjeeling (25 tea bags)" },
                    { new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9"), 499m, new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), "USD", "Our friends at Puttabong have done a great job with this tea. It is owned by Jayshree and is located north of Darjeeling town.  This tea came towards the end of the First Flush season in April. Brisk yet flavorful. Hats off!", "Puttabong 1st Flush Darjeeling (cup)" },
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), 299m, new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), "USD", "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly lighter liquor, with sweet honey flavor.", "Organic Assam (25 tea bags)" },
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), 399m, new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), "USD", "Matcha powdered green tea has been the pride of Uji for several centuries. This organic grade is great for everyday use.", "Organic Matcha (25 tea bags)" },
                    { new Guid("e95543da-bb67-4859-8b98-d92041d58d8d"), 399m, new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), "USD", "Matcha powdered green tea has been the pride of Uji for several centuries. This organic grade is great for everyday use.", "Organic Matcha (cup)" }
                });

            migrationBuilder.InsertData(
                table: "WarehouseProducts",
                columns: new[] { "ProductId", "WarehouseId", "Count" },
                values: new object[,]
                {
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 9753 },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 2313 },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 1237 },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 1327 },
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 1231 },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 1235 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Name", "ProductId" },
                values: new object[,]
                {
                    { "323565D2-3C93-4E05-81FF-AC745E22AF9E.webp", new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e") },
                    { "5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE.webp", new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee") },
                    { "323565D2-3C93-4E05-81FF-AC745E22AF9E.webp", new Guid("41e5e396-6757-42f3-9149-3b084976545a") },
                    { "78E540DE-D2B3-4B1F-BB1E-988BE3245088.webp", new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9") },
                    { "78E540DE-D2B3-4B1F-BB1E-988BE3245088.webp", new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088") },
                    { "5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE.webp", new Guid("e95543da-bb67-4859-8b98-d92041d58d8d") }
                });

            migrationBuilder.InsertData(
                table: "ProductPickupMethods",
                columns: new[] { "PickupMethodName", "ProductId" },
                values: new object[,]
                {
                    { "Shipping", new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee") },
                    { "Pickup", new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee") },
                    { "In-Store", new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9") },
                    { "In-Store", new Guid("e95543da-bb67-4859-8b98-d92041d58d8d") },
                    { "Pickup", new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088") },
                    { "In-Store", new Guid("41e5e396-6757-42f3-9149-3b084976545a") },
                    { "Shipping", new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e") },
                    { "Pickup", new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e") },
                    { "Shipping", new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088") }
                });

            migrationBuilder.InsertData(
                table: "WarehouseProducts",
                columns: new[] { "ProductId", "WarehouseId", "Count" },
                values: new object[,]
                {
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 0 },
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 7865 },
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 2131 },
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 1239 },
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 6655 },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickupMethodName",
                table: "Orders",
                column: "PickupMethodName");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreId",
                table: "Orders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ParentCategoryId",
                table: "ProductCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_TenantId",
                table: "ProductCategories",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPickupMethods_PickupMethodName",
                table: "ProductPickupMethods",
                column: "PickupMethodName");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_WarehouseId",
                table: "Stores",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantHosts_TenantId",
                table: "TenantHosts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantRoleClaims_RoleId",
                table: "TenantRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantRoles_TenantId",
                table: "TenantRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "TenantRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserClaims_UserId",
                table: "TenantUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserLogins_UserId",
                table: "TenantUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserRoles_RoleId",
                table: "TenantUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "TenantUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUsers_TenantId",
                table: "TenantUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "TenantUsers",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_ProductId",
                table: "WarehouseProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_TenantId",
                table: "Warehouses",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductPickupMethods");

            migrationBuilder.DropTable(
                name: "TenantHosts");

            migrationBuilder.DropTable(
                name: "TenantRoleClaims");

            migrationBuilder.DropTable(
                name: "TenantUserClaims");

            migrationBuilder.DropTable(
                name: "TenantUserLogins");

            migrationBuilder.DropTable(
                name: "TenantUserRoles");

            migrationBuilder.DropTable(
                name: "TenantUserTokens");

            migrationBuilder.DropTable(
                name: "WarehouseProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "TenantRoles");

            migrationBuilder.DropTable(
                name: "TenantUsers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PickupMethods");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
