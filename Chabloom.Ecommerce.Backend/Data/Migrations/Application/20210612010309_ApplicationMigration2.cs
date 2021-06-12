using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.Application
{
    public partial class ApplicationMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderProducts",
                newName: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("30f42a18-8821-4913-b562-33d46d28f158"),
                column: "ConcurrencyStamp",
                value: "4ea3f30b-23ff-4214-a54a-11698e49db5a");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"),
                column: "ConcurrencyStamp",
                value: "6197c6ac-0d80-4690-9c4f-33c8c36f2e33");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"),
                column: "ConcurrencyStamp",
                value: "c64904e7-7f1e-43ef-9d19-922ea3f4b862");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"),
                column: "ConcurrencyStamp",
                value: "b3677c7f-e554-4b54-9874-9aa5b714cb84");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderProducts",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("30f42a18-8821-4913-b562-33d46d28f158"),
                column: "ConcurrencyStamp",
                value: "f2b61b3f-7ec8-4362-95d0-2339a0be17dc");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"),
                column: "ConcurrencyStamp",
                value: "8c88d7f8-4a07-4984-89c5-38ca852af91a");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"),
                column: "ConcurrencyStamp",
                value: "38c0fc9b-4470-4a90-97b9-6f3fb53ca1e4");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"),
                column: "ConcurrencyStamp",
                value: "b04d3a93-9b97-4a67-9ae4-b8f30e60370a");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");
        }
    }
}
