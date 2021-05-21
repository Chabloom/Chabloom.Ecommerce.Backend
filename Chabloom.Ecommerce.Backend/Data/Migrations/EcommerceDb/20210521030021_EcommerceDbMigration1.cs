using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration1 : Migration
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
                name: "Roles",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleName",
                        column: x => x.RoleName,
                        principalTable: "Roles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Tenants_TenantId",
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantRoles_Tenants_TenantId",
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
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PickupMethodName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
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
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
                name: "TenantRoleUsers",
                columns: table => new
                {
                    TenantRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantRoleUsers", x => new { x.TenantRoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TenantRoleUsers_TenantRoles_TenantRoleId",
                        column: x => x.TenantRoleId,
                        principalTable: "TenantRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantRoleUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CurrencyId = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Filename = table.Column<string>(type: "text", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPickupMethod",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    PickupMethodName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPickupMethod", x => new { x.ProductId, x.PickupMethodName });
                    table.ForeignKey(
                        name: "FK_ProductPickupMethod_PickupMethods_PickupMethodName",
                        column: x => x.PickupMethodName,
                        principalTable: "PickupMethods",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPickupMethod_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseProducts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
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
                table: "Roles",
                column: "Name",
                values: new object[]
                {
                    "Admin",
                    "Manager"
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Joe's Tea Shop", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Augusta Green Jackets", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Description", "Name", "ParentCategoryId", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "While black teas are made from the same Camellia sinensis plant as all teas, the oxidation and processing is what distinguishes black teas from the rest. Premium black teas are withered, rolled, oxidized and fired in an oven, creating a warm and toasty flavor. The lengthier oxidation process causes the tea leaves to develop into dark brown and black colors. The flavors can range from malty or smokey to fruity and sweet. Black teas range from mellow teas from China to full-bodied teas from Assam, India.", "Black", null, new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "All green teas originate from the same species, the Camelia Sinensis. To make green tea, the fresh tea leaves are briefly cooked using either steam or dry heat. This process fixes the green colors and fresh flavors. The Chinese green teas are more mellow and smooth, while the Japanese green teas have the heft of rich, vegetal flavors, which comes from preservation of the chlorophyll.The general rule is that a cup of green tea contains about one-third as much caffeine as a cup of coffee. Green tea production methods vary but the focus is always to fix the green color. Thus, green teas are not oxidized.", "Green", null, new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Herbal teas, also known as herbal infusions, are typically a blend of herbs, flowers, spices, and dried fruit. This blend of ingredients is then brewed in the same way as your favorite traditional tea, either loose or in tea sachets or bags. By combining quality ingredients, blends can be created that calm, invigorate, or treat minor ailments. Colors and flavors range from light and fruity to vibrant and spicy, to match your mood.", "Herbal", null, new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Drinks", "Drinks", null, new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Food", "Food", null, new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Address", "CreatedTimestamp", "CreatedUser", "Description", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("69070b35-9ed3-47dd-a919-300371f54634"), "201 N Tryon St, Charlotte, NC 28202", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Charlotte Store", "Charlotte Store", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), "199 Gough St, San Francisco, CA 94102", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "San Fransisco Store", "San Fransisco Store", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "TenantRoles",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("30f42a18-8821-4913-b562-33d46d28f158"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Address", "CreatedTimestamp", "CreatedUser", "Description", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), "500 Great SW Pkwy SW, Atlanta, GA 30336", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Atlanta Warehouse", "Atlanta Warehouse", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), "650 Gateway Center Dr, San Diego, CA 92102", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "San Diego Warehouse", "San Diego Warehouse", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Description", "Name", "ParentCategoryId", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Assam is India's largest producer of tea, and the broad flood plains make for some of the most fertile tea estates in the world.", "Assam", new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "The light and brisk First Flush teas focus your attention. While the darker teas of the later seasons are more mellow and often have wonderful flavors.", "Darjeeling", new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Powdered green teas have been consumed in China and Japan for centuries. However it is only in the last few decades that Westerners have acquired a taste for this ancient tea. We enjoy the bracing vegetal flavors, as well as the unusual process for preparing the tea.", "Matcha", new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedTimestamp", "CreatedUser", "CurrencyId", "Description", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), 299m, new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "It seems like it is Yuzu’s time to shine. People are really liking this citrus fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we should try it.", "Yuzu Sencha (25 tea bags)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a"), 299m, new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "It seems like it is Yuzu’s time to shine. People are really liking this citrus fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we should try it.", "Yuzu Sencha (cup)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), 299m, new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It looks festive brewed in a glass teapot, and tastes delicious hot or iced. ", "Strawberry Kiwi Fruit Tea (25 tea bags)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3"), 299m, new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It looks festive brewed in a glass teapot, and tastes delicious hot or iced. ", "Strawberry Kiwi Fruit Tea (cup)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), 299m, new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and distinctive twist found in blood oranges. Delicious hot or cold, it brews an aromatic and vivid shade of orange.", "Blood Orange Fruit Tea (25 tea bags)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e"), 299m, new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and distinctive twist found in blood oranges. Delicious hot or cold, it brews an aromatic and vivid shade of orange.", "Blood Orange Fruit Tea (cup)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969"), 599m, new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Bud Light is a premium beer with incredible drinkability that has made it a top selling American beer that everybody knows and loves. This light beer is brewed using a combination of barley malts, rice and a blend of premium aroma hop varieties. Featuring a fresh, clean taste with subtle hop aromas, this light lager delivers ultimate refreshment with its delicate malt sweetness and crisp finish.", "Beer", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00"), 499m, new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Designed to be a great tasting water, our water is filtered by reverse osmosis to remove impurities, then enhanced with a special blend of minerals for a pure, crisp, fresh taste.", "Water", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("781d9646-1156-4cbb-a581-329f2ae34744"), 399m, new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "The original burger starts with a 100% pure beef burger seasoned with just a pinch of salt and pepper. Then, the burger is topped with a tangy pickle, chopped onions, ketchup and mustard.", "Hamburger", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7"), 199m, new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Our tasty all beef hot dogs are all natural, skinless, uncured and made with beef that’s never given antibiotics.", "Hot dog", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Filename", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("ee465f84-413c-41f9-a3da-a149f241bdfa"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "CE3E245B-75C5-418E-98FE-3A115AA7395D.webp", new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d3fde56f-5393-4641-b573-a3e71960d542"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "C615100B-E2D9-48A4-81C1-824A3BB12CB7.webp", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6f054d9a-f2b9-49b0-86fa-71f328dd2daf"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "0321E99E-DD3B-402F-9CF6-E2BA284862D0.webp", new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("75a0c5a5-b91a-4c0f-a573-9c8cfe70ee7a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "781D9646-1156-4CBB-A581-329F2AE34744.webp", new Guid("781d9646-1156-4cbb-a581-329f2ae34744"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("3e8b9e9f-6a32-4262-96c7-784b8ea455e8"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "0AECBBC7-0E6C-4727-BC05-9D3700397B00.webp", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5b605894-7618-4b97-a49b-9202f6e2799a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "CB949DDA-57FB-4731-8379-B6F955B3102E.webp", new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c1779df8-8cc8-425d-9fea-dca8531fd228"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "0321E99E-DD3B-402F-9CF6-E2BA284862D0.webp", new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("745b7c71-03af-4ada-b18c-e370e5305e64"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "CE3E245B-75C5-418E-98FE-3A115AA7395D.webp", new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a030e5d7-45a2-407c-ac56-b7f310542fb9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "CB949DDA-57FB-4731-8379-B6F955B3102E.webp", new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("84519e6f-1c4e-426b-90e7-f3b444a5ce79"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "9AA49AE2-53BB-417A-B1F7-1BD9F6578969.webp", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProductPickupMethod",
                columns: new[] { "PickupMethodName", "ProductId" },
                values: new object[,]
                {
                    { "In-Store", new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3") },
                    { "Pickup", new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0") },
                    { "Shipping", new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0") },
                    { "Pickup", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969") },
                    { "In-Store", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969") },
                    { "Pickup", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00") },
                    { "In-Store", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00") },
                    { "Pickup", new Guid("781d9646-1156-4cbb-a581-329f2ae34744") },
                    { "In-Store", new Guid("781d9646-1156-4cbb-a581-329f2ae34744") },
                    { "In-Store", new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e") },
                    { "Pickup", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7") },
                    { "In-Store", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7") },
                    { "Pickup", new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d") },
                    { "Pickup", new Guid("cb949dda-57fb-4731-8379-b6f955b3102e") },
                    { "Shipping", new Guid("cb949dda-57fb-4731-8379-b6f955b3102e") },
                    { "Shipping", new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d") },
                    { "In-Store", new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedTimestamp", "CreatedUser", "CurrencyId", "Description", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), 299m, new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly lighter liquor, with sweet honey flavor.", "Organic Assam (25 tea bags)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("41e5e396-6757-42f3-9149-3b084976545a"), 299m, new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly lighter liquor, with sweet honey flavor.", "Organic Assam (cup)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e95543da-bb67-4859-8b98-d92041d58d8d"), 399m, new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Matcha powdered green tea has been the pride of Uji for several centuries. This organic grade is great for everyday use.", "Organic Matcha (cup)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), 399m, new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Matcha powdered green tea has been the pride of Uji for several centuries. This organic grade is great for everyday use.", "Organic Matcha (25 tea bags)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), 499m, new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Our friends at Puttabong have done a great job with this tea. It is owned by Jayshree and is located north of Darjeeling town.  This tea came towards the end of the First Flush season in April. Brisk yet flavorful. Hats off!", "Puttabong 1st Flush Darjeeling (25 tea bags)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9"), 499m, new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "Our friends at Puttabong have done a great job with this tea. It is owned by Jayshree and is located north of Darjeeling town.  This tea came towards the end of the First Flush season in April. Brisk yet flavorful. Hats off!", "Puttabong 1st Flush Darjeeling (cup)", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StoreProducts",
                columns: new[] { "Id", "Count", "CreatedTimestamp", "CreatedUser", "ProductId", "StoreId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("0e3ed181-db00-4003-b681-1de443217ed4"), 33, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("53b6860e-1611-4859-803f-5f6aaf45f5e0"), 55, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0062e9e5-c772-4ed3-8ae2-0538934408c1"), 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("80698015-c9b4-4678-ba49-dfc570a758bc"), 123, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ad1b779a-daf3-45ed-a23c-2a294f92a28d"), 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("95989b24-177d-44c8-9b29-3ed56147c524"), 22, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "WarehouseProducts",
                columns: new[] { "Id", "Count", "CreatedTimestamp", "CreatedUser", "ProductId", "UpdatedTimestamp", "UpdatedUser", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("291f2b10-43c4-48f8-8984-ae77c6a15021"), 1235, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("788b957b-033c-455b-9ec7-e9f866f022fe"), 1231, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("6e4ea917-5e80-414e-bc57-0a47f4a6cb8d"), 1237, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("41533622-4fb8-4c5b-889a-459681765aaf"), 1327, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("736d93a3-4f54-416a-ab0d-3b2bd8f7c45d"), 9753, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("d3f3aece-660e-482f-a24e-544ea07b516a"), 2313, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Filename", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("9a467cbc-8128-44be-addd-b85f65f57cad"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "78E540DE-D2B3-4B1F-BB1E-988BE3245088.webp", new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("622d3d2c-1ff2-43b8-94e8-36ae7c2ca86b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE.webp", new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("de6cac85-5c34-4ea6-b1d5-b1c8ec111070"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "323565D2-3C93-4E05-81FF-AC745E22AF9E.webp", new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0d7936d7-0d94-4f31-9314-135ea1daf3c9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "78E540DE-D2B3-4B1F-BB1E-988BE3245088.webp", new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ef15fab8-2ce0-4bbf-a8e1-d8e1b541ce6a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "323565D2-3C93-4E05-81FF-AC745E22AF9E.webp", new Guid("41e5e396-6757-42f3-9149-3b084976545a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0aabec17-aaa1-4f35-9b27-9b199a2aa67c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE.webp", new Guid("e95543da-bb67-4859-8b98-d92041d58d8d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProductPickupMethod",
                columns: new[] { "PickupMethodName", "ProductId" },
                values: new object[,]
                {
                    { "Shipping", new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee") },
                    { "Pickup", new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee") },
                    { "In-Store", new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9") },
                    { "Shipping", new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088") },
                    { "Pickup", new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088") },
                    { "In-Store", new Guid("e95543da-bb67-4859-8b98-d92041d58d8d") },
                    { "In-Store", new Guid("41e5e396-6757-42f3-9149-3b084976545a") },
                    { "Shipping", new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e") },
                    { "Pickup", new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e") }
                });

            migrationBuilder.InsertData(
                table: "StoreProducts",
                columns: new[] { "Id", "Count", "CreatedTimestamp", "CreatedUser", "ProductId", "StoreId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("08f67210-a91b-486c-87dd-af4115e54172"), 15, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("24e60557-5726-4fb5-a8f6-9c64496d016d"), 78, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6f853ff0-1d4c-4905-94f7-cd20e3b18392"), 11, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0e3ebfd7-99f3-4d7c-87b2-c0f17dc34731"), 512, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("139b4e0d-0c3d-47eb-a7a6-8929608a6545"), 66, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("67df3761-d6c8-428a-a614-e737d254c89c"), 89, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "WarehouseProducts",
                columns: new[] { "Id", "Count", "CreatedTimestamp", "CreatedUser", "ProductId", "UpdatedTimestamp", "UpdatedUser", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("fb007436-3430-4dcf-bbff-075040ebd8ed"), 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("1adb29fd-a90d-4c00-86af-038ab3e0e024"), 1239, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("dc710497-c38a-49f1-bbb6-91fb58f7630d"), 6655, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("aca91fc8-d5aa-4376-9921-f12917434d33"), 7865, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("bce64f3a-b012-48d3-89f2-36b84d5f133c"), 2131, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("a43482c7-9549-4f5c-816f-2a6ba40d5115"), 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId_ProductId",
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickupMethodName",
                table: "Orders",
                column: "PickupMethodName");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

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
                name: "IX_ProductPickupMethod_PickupMethodName",
                table: "ProductPickupMethod",
                column: "PickupMethodName");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_ProductId",
                table: "StoreProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_StoreId_ProductId",
                table: "StoreProducts",
                columns: new[] { "StoreId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_TenantId",
                table: "Stores",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantRoles_TenantId",
                table: "TenantRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantRoleUsers_UserId",
                table: "TenantRoleUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleName",
                table: "Users",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_ProductId",
                table: "WarehouseProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_WarehouseId_ProductId",
                table: "WarehouseProducts",
                columns: new[] { "WarehouseId", "ProductId" },
                unique: true);

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
                name: "ProductPickupMethod");

            migrationBuilder.DropTable(
                name: "StoreProducts");

            migrationBuilder.DropTable(
                name: "TenantRoleUsers");

            migrationBuilder.DropTable(
                name: "WarehouseProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "TenantRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "PickupMethods");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
