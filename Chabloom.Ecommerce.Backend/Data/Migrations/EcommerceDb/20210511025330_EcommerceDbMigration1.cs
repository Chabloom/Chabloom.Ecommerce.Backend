using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EcommercePickupMethods",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommercePickupMethods", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceRoles",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceRoles", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "character varying(255)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceUsers_EcommerceRoles_RoleName",
                        column: x => x.RoleName,
                        principalTable: "EcommerceRoles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
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
                    table.PrimaryKey("PK_EcommerceProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceProductCategories_EcommerceProductCategories_Paren~",
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PickupMethodName = table.Column<string>(type: "character varying(255)", nullable: false),
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
                    table.PrimaryKey("PK_EcommerceOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceOrders_EcommercePickupMethods_PickupMethodName",
                        column: x => x.PickupMethodName,
                        principalTable: "EcommercePickupMethods",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcommerceOrders_EcommerceUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "EcommerceUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
                    TenantRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
                    table.PrimaryKey("PK_EcommerceProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceProductImages_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceProductPickupMethods",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    PickupMethodName = table.Column<string>(type: "character varying(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceProductPickupMethods", x => new { x.ProductId, x.PickupMethodName });
                    table.ForeignKey(
                        name: "FK_EcommerceProductPickupMethods_EcommercePickupMethods_Pickup~",
                        column: x => x.PickupMethodName,
                        principalTable: "EcommercePickupMethods",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcommerceProductPickupMethods_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceStoreProducts",
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
                table: "EcommercePickupMethods",
                column: "Name",
                values: new object[]
                {
                    "In-Store",
                    "Pickup",
                    "Delivery",
                    "Shipping"
                });

            migrationBuilder.InsertData(
                table: "EcommerceRoles",
                column: "Name",
                values: new object[]
                {
                    "Admin",
                    "Manager"
                });

            migrationBuilder.InsertData(
                table: "EcommerceTenants",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Joe's Tea Shop", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Augusta Green Jackets", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductCategories",
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
                    { new Guid("30f42a18-8821-4913-b562-33d46d28f158"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
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
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "It seems like it is Yuzu’s time to shine. People are really liking this citrus fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we should try it.", "Yuzu Sencha (25 tea bags)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a"), new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "It seems like it is Yuzu’s time to shine. People are really liking this citrus fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we should try it.", "Yuzu Sencha (cup)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It looks festive brewed in a glass teapot, and tastes delicious hot or iced. ", "Strawberry Kiwi Fruit Tea (25 tea bags)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It looks festive brewed in a glass teapot, and tastes delicious hot or iced. ", "Strawberry Kiwi Fruit Tea (cup)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and distinctive twist found in blood oranges. Delicious hot or cold, it brews an aromatic and vivid shade of orange.", "Blood Orange Fruit Tea (25 tea bags)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and distinctive twist found in blood oranges. Delicious hot or cold, it brews an aromatic and vivid shade of orange.", "Blood Orange Fruit Tea (cup)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969"), new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Bud Light is a premium beer with incredible drinkability that has made it a top selling American beer that everybody knows and loves. This light beer is brewed using a combination of barley malts, rice and a blend of premium aroma hop varieties. Featuring a fresh, clean taste with subtle hop aromas, this light lager delivers ultimate refreshment with its delicate malt sweetness and crisp finish.", "Beer", 5.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00"), new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Designed to be a great tasting water, our water is filtered by reverse osmosis to remove impurities, then enhanced with a special blend of minerals for a pure, crisp, fresh taste.", "Water", 4.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("781d9646-1156-4cbb-a581-329f2ae34744"), new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "The original burger starts with a 100% pure beef burger seasoned with just a pinch of salt and pepper. Then, the burger is topped with a tangy pickle, chopped onions, ketchup and mustard.", "Hamburger", 3.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7"), new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our tasty all beef hot dogs are all natural, skinless, uncured and made with beef that’s never given antibiotics.", "Hot dog", 1.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductImages",
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
                table: "EcommerceProductPickupMethods",
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
                table: "EcommerceProducts",
                columns: new[] { "Id", "CategoryId", "CreatedTimestamp", "CreatedUser", "Description", "Name", "Price", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly lighter liquor, with sweet honey flavor.", "Organic Assam (25 tea bags)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("41e5e396-6757-42f3-9149-3b084976545a"), new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly lighter liquor, with sweet honey flavor.", "Organic Assam (cup)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e95543da-bb67-4859-8b98-d92041d58d8d"), new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Matcha powdered green tea has been the pride of Uji for several centuries. This organic grade is great for everyday use.", "Organic Matcha (cup)", 3.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Matcha powdered green tea has been the pride of Uji for several centuries. This organic grade is great for everyday use.", "Organic Matcha (25 tea bags)", 3.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our friends at Puttabong have done a great job with this tea. It is owned by Jayshree and is located north of Darjeeling town.  This tea came towards the end of the First Flush season in April. Brisk yet flavorful. Hats off!", "Puttabong 1st Flush Darjeeling (25 tea bags)", 4.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9"), new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our friends at Puttabong have done a great job with this tea. It is owned by Jayshree and is located north of Darjeeling town.  This tea came towards the end of the First Flush season in April. Brisk yet flavorful. Hats off!", "Puttabong 1st Flush Darjeeling (cup)", 4.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceStoreProducts",
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
                table: "EcommerceWarehouseProducts",
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
                table: "EcommerceProductImages",
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
                table: "EcommerceProductPickupMethods",
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
                table: "EcommerceStoreProducts",
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
                table: "EcommerceWarehouseProducts",
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
                name: "IX_EcommerceOrderProducts_OrderId_ProductId",
                table: "EcommerceOrderProducts",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceOrderProducts_ProductId",
                table: "EcommerceOrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceOrders_PickupMethodName",
                table: "EcommerceOrders",
                column: "PickupMethodName");

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
                name: "IX_EcommerceProductPickupMethods_PickupMethodName",
                table: "EcommerceProductPickupMethods",
                column: "PickupMethodName");

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
                unique: true);

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
                name: "IX_EcommerceUsers_RoleName",
                table: "EcommerceUsers",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceWarehouseProducts_ProductId",
                table: "EcommerceWarehouseProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceWarehouseProducts_WarehouseId_ProductId",
                table: "EcommerceWarehouseProducts",
                columns: new[] { "WarehouseId", "ProductId" },
                unique: true);

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
                name: "EcommerceProductPickupMethods");

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
                name: "EcommercePickupMethods");

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
