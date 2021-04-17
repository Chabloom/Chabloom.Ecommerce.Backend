using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EcommerceTenants",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Augusta Green Jackets", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "EcommerceProductCategories",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Description", "Name", "ParentCategoryId", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Drinks", "Drinks", null, new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Food", "Food", null, new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceTenantRoles",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProducts",
                columns: new[] { "Id", "CategoryId", "CreatedTimestamp", "CreatedUser", "Description", "Name", "Price", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969"), new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Beer", "Beer", 5.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00"), new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Water", "Water", 4.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("781d9646-1156-4cbb-a581-329f2ae34744"), new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Hamburger", "Hamburger", 3.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7"), new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Hot dog", "Hot dog", 1.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Filename", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("84519e6f-1c4e-426b-90e7-f3b444a5ce79"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "9AA49AE2-53BB-417A-B1F7-1BD9F6578969.webp", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("3e8b9e9f-6a32-4262-96c7-784b8ea455e8"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "0AECBBC7-0E6C-4727-BC05-9D3700397B00.webp", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("75a0c5a5-b91a-4c0f-a573-9c8cfe70ee7a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "781D9646-1156-4CBB-A581-329F2AE34744.webp", new Guid("781d9646-1156-4cbb-a581-329f2ae34744"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d3fde56f-5393-4641-b573-a3e71960d542"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "C615100B-E2D9-48A4-81C1-824A3BB12CB7.webp", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductPickupMethods",
                columns: new[] { "PickupMethodName", "ProductId" },
                values: new object[,]
                {
                    { "Pickup", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969") },
                    { "In-Store", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969") },
                    { "Pickup", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00") },
                    { "In-Store", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00") },
                    { "Pickup", new Guid("781d9646-1156-4cbb-a581-329f2ae34744") },
                    { "In-Store", new Guid("781d9646-1156-4cbb-a581-329f2ae34744") },
                    { "Pickup", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7") },
                    { "In-Store", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("3e8b9e9f-6a32-4262-96c7-784b8ea455e8"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("75a0c5a5-b91a-4c0f-a573-9c8cfe70ee7a"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("84519e6f-1c4e-426b-90e7-f3b444a5ce79"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("d3fde56f-5393-4641-b573-a3e71960d542"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductPickupMethods",
                keyColumns: new[] { "PickupMethodName", "ProductId" },
                keyValues: new object[] { "In-Store", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00") });

            migrationBuilder.DeleteData(
                table: "EcommerceProductPickupMethods",
                keyColumns: new[] { "PickupMethodName", "ProductId" },
                keyValues: new object[] { "Pickup", new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00") });

            migrationBuilder.DeleteData(
                table: "EcommerceProductPickupMethods",
                keyColumns: new[] { "PickupMethodName", "ProductId" },
                keyValues: new object[] { "In-Store", new Guid("781d9646-1156-4cbb-a581-329f2ae34744") });

            migrationBuilder.DeleteData(
                table: "EcommerceProductPickupMethods",
                keyColumns: new[] { "PickupMethodName", "ProductId" },
                keyValues: new object[] { "Pickup", new Guid("781d9646-1156-4cbb-a581-329f2ae34744") });

            migrationBuilder.DeleteData(
                table: "EcommerceProductPickupMethods",
                keyColumns: new[] { "PickupMethodName", "ProductId" },
                keyValues: new object[] { "In-Store", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969") });

            migrationBuilder.DeleteData(
                table: "EcommerceProductPickupMethods",
                keyColumns: new[] { "PickupMethodName", "ProductId" },
                keyValues: new object[] { "Pickup", new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969") });

            migrationBuilder.DeleteData(
                table: "EcommerceProductPickupMethods",
                keyColumns: new[] { "PickupMethodName", "ProductId" },
                keyValues: new object[] { "In-Store", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7") });

            migrationBuilder.DeleteData(
                table: "EcommerceProductPickupMethods",
                keyColumns: new[] { "PickupMethodName", "ProductId" },
                keyValues: new object[] { "Pickup", new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7") });

            migrationBuilder.DeleteData(
                table: "EcommerceTenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"));

            migrationBuilder.DeleteData(
                table: "EcommerceTenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("781d9646-1156-4cbb-a581-329f2ae34744"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("1def1630-85ef-4a97-a073-fd3ba814bab0"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("f2b822fc-4e6e-4c65-a5bb-74d080c9e33a"));

            migrationBuilder.DeleteData(
                table: "EcommerceTenants",
                keyColumn: "Id",
                keyValue: new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c"));
        }
    }
}
