using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "EcommerceStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceStores_EcommerceTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "EcommerceTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceWarehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcommerceWarehouses_EcommerceTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "EcommerceTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceOrderProducts",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceOrderProducts", x => new { x.OrderId, x.ProductId });
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceStoreProducts",
                columns: table => new
                {
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceStoreProducts", x => new { x.StoreId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_EcommerceStoreProducts_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceWarehouseProducts", x => new { x.WarehouseId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_EcommerceWarehouseProducts_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcommerceWarehouseProducts_EcommerceWarehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "EcommerceWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EcommerceStores",
                columns: new[] { "Id", "Address", "CreatedTimestamp", "CreatedUser", "Description", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("69070b35-9ed3-47dd-a919-300371f54634"), "201 N Tryon St, Charlotte, NC 28202", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Charlotte Store", "Charlotte Store", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), "199 Gough St, San Francisco, CA 94102", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "San Fransisco Store", "San Fransisco Store", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceWarehouses",
                columns: new[] { "Id", "Address", "CreatedTimestamp", "CreatedUser", "Description", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), "500 Great SW Pkwy SW, Atlanta, GA 30336", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Atlanta Warehouse", "Atlanta Warehouse", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), "650 Gateway Center Dr, San Diego, CA 92102", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "San Diego Warehouse", "San Diego Warehouse", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceStoreProducts",
                columns: new[] { "ProductId", "StoreId", "Count" },
                values: new object[,]
                {
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), 89 },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), 33 },
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), 512 },
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), 22 },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), 15 },
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), 66 },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("92a73aca-281a-4ce2-9970-a1d6fbb75802"), 0 },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), 55 },
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), 11 },
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), 0 },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), 78 },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("69070b35-9ed3-47dd-a919-300371f54634"), 123 }
                });

            migrationBuilder.InsertData(
                table: "EcommerceWarehouseProducts",
                columns: new[] { "ProductId", "WarehouseId", "Count" },
                values: new object[,]
                {
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 6655 },
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 1231 },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 0 },
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 7865 },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 1237 },
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 1239 },
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 9753 },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 0 },
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 2131 },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 1235 },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("95d98c98-8c88-4a15-b3ae-9ddb9b10848b"), 1327 },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("68a72052-18f4-4e2a-a165-c057f61f86b5"), 2313 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceOrderProducts_ProductId",
                table: "EcommerceOrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceOrders_UserId",
                table: "EcommerceOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceStoreProducts_ProductId",
                table: "EcommerceStoreProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceStores_TenantId",
                table: "EcommerceStores",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceWarehouseProducts_ProductId",
                table: "EcommerceWarehouseProducts",
                column: "ProductId");

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
                name: "EcommerceStoreProducts");

            migrationBuilder.DropTable(
                name: "EcommerceWarehouseProducts");

            migrationBuilder.DropTable(
                name: "EcommerceOrders");

            migrationBuilder.DropTable(
                name: "EcommerceStores");

            migrationBuilder.DropTable(
                name: "EcommerceWarehouses");
        }
    }
}
