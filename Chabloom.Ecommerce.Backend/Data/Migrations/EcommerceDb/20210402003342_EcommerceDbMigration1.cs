using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EcommerceRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceUsers_EcommerceRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EcommerceRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceProductCategories_EcommerceProductCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "EcommerceProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcommerceProductCategories_EcommerceTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "EcommerceTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceStores_EcommerceTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "EcommerceTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceTenantRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceTenantRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceTenantRoles_EcommerceTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "EcommerceTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceWarehouses_EcommerceTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "EcommerceTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceOrders_EcommerceUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "EcommerceUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceProducts_EcommerceProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EcommerceProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceTenantRoleUsers",
                columns: table => new
                {
                    TenantRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceTenantRoleUsers", x => new { x.TenantRoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_EcommerceTenantRoleUsers_EcommerceTenantRoles_TenantRoleId",
                        column: x => x.TenantRoleId,
                        principalTable: "EcommerceTenantRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcommerceTenantRoleUsers_EcommerceUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "EcommerceUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceOrderProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceOrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceOrderProducts_EcommerceOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "EcommerceOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcommerceOrderProducts_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceProductImages_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceStoreProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceStoreProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceStoreProducts_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcommerceStoreProducts_EcommerceStores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "EcommerceStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceWarehouseProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceWarehouseProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceWarehouseProducts_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcommerceWarehouseProducts_EcommerceWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "EcommerceWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EcommerceRoles",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("d4dc0126-c55d-474e-a44b-7e6d90822a59"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "EcommerceRoles",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("5ab67841-f85c-416f-8a81-81b85bb7b219"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "EcommerceTenants",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Joe's Tea Shop", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "EcommerceProductCategories",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Description", "Name", "ParentCategoryId", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "While black teas are made from the same Camellia sinensis plant as all teas, the oxidation and processing is what distinguishes black teas from the rest. Premium black teas are withered, rolled, oxidized and fired in an oven, creating a warm and toasty flavor. The lengthier oxidation process causes the tea leaves to develop into dark brown and black colors. The flavors can range from malty or smokey to fruity and sweet. Black teas range from mellow teas from China to full-bodied teas from Assam, India.", "Black", null, new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "All green teas originate from the same species, the Camelia Sinensis. To make green tea, the fresh tea leaves are briefly cooked using either steam or dry heat. This process fixes the green colors and fresh flavors. The Chinese green teas are more mellow and smooth, while the Japanese green teas have the heft of rich, vegetal flavors, which comes from preservation of the chlorophyll.The general rule is that a cup of green tea contains about one-third as much caffeine as a cup of coffee. Green tea production methods vary but the focus is always to fix the green color. Thus, green teas are not oxidized.", "Green", null, new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Herbal teas, also known as herbal infusions, are typically a blend of herbs, flowers, spices, and dried fruit. This blend of ingredients is then brewed in the same way as your favorite traditional tea, either loose or in tea sachets or bags. By combining quality ingredients, blends can be created that calm, invigorate, or treat minor ailments. Colors and flavors range from light and fruity to vibrant and spicy, to match your mood.", "Herbal", null, new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceStores",
                columns: new[] { "Id", "Address", "CreatedTimestamp", "CreatedUser", "Description", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("69070b35-9ed3-47dd-a919-300371f54634"), "201 N Tryon St, Charlotte, NC 28202", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Charlotte Store", "Charlotte Store", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), "199 Gough St, San Francisco, CA 94102", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "San Fransisco Store", "San Fransisco Store", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceTenantRoles",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("30f42a18-8821-4913-b562-33d46d28f158"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceWarehouses",
                columns: new[] { "Id", "Address", "CreatedTimestamp", "CreatedUser", "Description", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), "500 Great SW Pkwy SW, Atlanta, GA 30336", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Atlanta Warehouse", "Atlanta Warehouse", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), "650 Gateway Center Dr, San Diego, CA 92102", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "San Diego Warehouse", "San Diego Warehouse", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductCategories",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Description", "Name", "ParentCategoryId", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Assam is India's largest producer of tea, and the broad flood plains make for some of the most fertile tea estates in the world.", "Assam", new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "The light and brisk First Flush teas focus your attention. While the darker teas of the later seasons are more mellow and often have wonderful flavors.", "Darjeeling", new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Powdered green teas have been consumed in China and Japan for centuries. However it is only in the last few decades that Westerners have acquired a taste for this ancient tea. We enjoy the bracing vegetal flavors, as well as the unusual process for preparing the tea.", "Matcha", new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProducts",
                columns: new[] { "Id", "CategoryId", "CreatedTimestamp", "CreatedUser", "Description", "Name", "Price", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "It seems like it is Yuzu’s time to shine. People are really liking this citrus fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we should try it.", "Yuzu Sencha", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It looks festive brewed in a glass teapot, and tastes delicious hot or iced. ", "Strawberry Kiwi Fruit Tea", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and distinctive twist found in blood oranges. Delicious hot or cold, it brews an aromatic and vivid shade of orange.", "Blood Orange Fruit Tea", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("6f054d9a-f2b9-49b0-86fa-71f328dd2daf"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("745b7c71-03af-4ada-b18c-e370e5305e64"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5b605894-7618-4b97-a49b-9202f6e2799a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProducts",
                columns: new[] { "Id", "CategoryId", "CreatedTimestamp", "CreatedUser", "Description", "Name", "Price", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Matcha powdered green tea has been the pride of Uji for several centuries. This organic grade is great for everyday use.", "Organic Matcha", 3.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly lighter liquor, with sweet honey flavor.", "Organic Assam", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our friends at Puttabong have done a great job with this tea. It is owned by Jayshree and is located north of Darjeeling town.  This tea came towards the end of the First Flush season in April. Brisk yet flavorful. Hats off!", "Puttabong 1st Flush Darjeeling", 4.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceStoreProducts",
                columns: new[] { "Id", "Count", "ProductId", "StoreId" },
                values: new object[,]
                {
                    { new Guid("0ad5a574-f8a4-405c-9d64-ce5a127a3b84"), 22, new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802") },
                    { new Guid("a105ee03-3639-4efd-9104-040047175b21"), 0, new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("69070b35-9ed3-47dd-a919-300371f54634") },
                    { new Guid("e4571be3-9757-4c6f-82e0-e43a6658defc"), 33, new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802") },
                    { new Guid("fa7fd697-59cd-4e6d-a266-9511eae0f54b"), 123, new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("69070b35-9ed3-47dd-a919-300371f54634") },
                    { new Guid("be87833a-4cc7-4280-964c-596b82d31600"), 0, new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802") },
                    { new Guid("56575824-7eb7-4cf1-b3e5-246b8044d4ca"), 55, new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("69070b35-9ed3-47dd-a919-300371f54634") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceWarehouseProducts",
                columns: new[] { "Id", "Count", "ProductId", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("669f9739-c0df-4888-b9f3-9d6455a6774d"), 1231, new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("f275ef04-1de2-4bc0-84bc-b9d959a14377"), 2313, new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("ca524773-910e-425e-9d6a-5c40ff78f4e8"), 1327, new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("bd9e37f4-d650-4b14-a234-55d14beaac2f"), 1235, new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("34c06c9d-12a3-475e-b642-f09be45bf8ac"), 1237, new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("81cc7e11-c4c3-47d9-aa89-8c55bbdd1b3b"), 9753, new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("de6cac85-5c34-4ea6-b1d5-b1c8ec111070"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9a467cbc-8128-44be-addd-b85f65f57cad"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("622d3d2c-1ff2-43b8-94e8-36ae7c2ca86b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceStoreProducts",
                columns: new[] { "Id", "Count", "ProductId", "StoreId" },
                values: new object[,]
                {
                    { new Guid("15914c52-621d-4fdf-813a-6c580ca78d84"), 89, new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("69070b35-9ed3-47dd-a919-300371f54634") },
                    { new Guid("b1e88afc-1b2e-4e37-ae2a-6916d6bbbc88"), 66, new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802") },
                    { new Guid("3e8ca37d-a0a9-499d-bf0e-defa25fe170c"), 78, new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("69070b35-9ed3-47dd-a919-300371f54634") },
                    { new Guid("4e227039-ef95-4da3-9425-a2fda003439e"), 15, new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802") },
                    { new Guid("b676ec60-143b-4469-a1dd-d0acc0ee5464"), 11, new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("69070b35-9ed3-47dd-a919-300371f54634") },
                    { new Guid("f5b4a8f6-49ca-4572-b80c-7136c0ff9f6d"), 512, new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceWarehouseProducts",
                columns: new[] { "Id", "Count", "ProductId", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("4534577c-81ae-4448-8a6e-867c98880616"), 2131, new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("47168e9a-78fc-4027-9eed-13b16ca18259"), 7865, new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("8dc63f9f-7a6b-4641-879c-2df45d639344"), 0, new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("608a8fca-9e3d-422d-9ce0-99e3c5999b6e"), 0, new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") },
                    { new Guid("d25de491-6041-441d-90ca-99dfaba97075"), 1239, new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b") },
                    { new Guid("7ee4a1a6-d9d0-4a32-bf6f-8107410e37d8"), 6655, new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceOrderProducts_OrderId_ProductId",
                table: "EcommerceOrderProducts",
                columns: new[] { "OrderId", "ProductId" },
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceOrderProducts_ProductId",
                table: "EcommerceOrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceOrders_UserId",
                table: "EcommerceOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceProductCategories_ParentCategoryId",
                table: "EcommerceProductCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceProductCategories_TenantId",
                table: "EcommerceProductCategories",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceProductImages_ProductId",
                table: "EcommerceProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceProducts_CategoryId",
                table: "EcommerceProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceStoreProducts_ProductId",
                table: "EcommerceStoreProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceStoreProducts_StoreId_ProductId",
                table: "EcommerceStoreProducts",
                columns: new[] { "StoreId", "ProductId" },
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceStores_TenantId",
                table: "EcommerceStores",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceTenantRoles_TenantId",
                table: "EcommerceTenantRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceTenantRoleUsers_UserId",
                table: "EcommerceTenantRoleUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceUsers_RoleId",
                table: "EcommerceUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceWarehouseProducts_ProductId",
                table: "EcommerceWarehouseProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceWarehouseProducts_WarehouseId_ProductId",
                table: "EcommerceWarehouseProducts",
                columns: new[] { "WarehouseId", "ProductId" },
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceWarehouses_TenantId",
                table: "EcommerceWarehouses",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EcommerceOrderProducts");

            migrationBuilder.DropTable(
                name: "EcommerceProductImages");

            migrationBuilder.DropTable(
                name: "EcommerceStoreProducts");

            migrationBuilder.DropTable(
                name: "EcommerceTenantRoleUsers");

            migrationBuilder.DropTable(
                name: "EcommerceWarehouseProducts");

            migrationBuilder.DropTable(
                name: "EcommerceOrders");

            migrationBuilder.DropTable(
                name: "EcommerceStores");

            migrationBuilder.DropTable(
                name: "EcommerceTenantRoles");

            migrationBuilder.DropTable(
                name: "EcommerceProducts");

            migrationBuilder.DropTable(
                name: "EcommerceWarehouses");

            migrationBuilder.DropTable(
                name: "EcommerceUsers");

            migrationBuilder.DropTable(
                name: "EcommerceProductCategories");

            migrationBuilder.DropTable(
                name: "EcommerceRoles");

            migrationBuilder.DropTable(
                name: "EcommerceTenants");
        }
    }
}
