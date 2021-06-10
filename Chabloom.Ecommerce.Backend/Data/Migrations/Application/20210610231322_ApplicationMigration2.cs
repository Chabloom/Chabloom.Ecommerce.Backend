using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.Application
{
    public partial class ApplicationMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Warehouses");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("30f42a18-8821-4913-b562-33d46d28f158"),
                column: "ConcurrencyStamp",
                value: "77831f89-4bda-4a80-95a9-147b7af19fbe");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"),
                column: "ConcurrencyStamp",
                value: "a4f3be9d-84f3-4663-ae11-ef1279854de3");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"),
                column: "ConcurrencyStamp",
                value: "843ae13e-5abe-4eae-81e7-6725442a6cf6");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"),
                column: "ConcurrencyStamp",
                value: "90716d72-bbf2-4ae0-bdbf-5637f89f2dac");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Warehouses",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("30f42a18-8821-4913-b562-33d46d28f158"),
                column: "ConcurrencyStamp",
                value: "0bce3440-3bba-40fe-9fff-0521111ab84a");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"),
                column: "ConcurrencyStamp",
                value: "aeb230bc-8359-4e0c-8ee1-37f9b85641a5");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"),
                column: "ConcurrencyStamp",
                value: "6fa2d4e4-f022-46ab-843d-5b6943d33f85");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"),
                column: "ConcurrencyStamp",
                value: "f058d1f1-376e-4f54-8524-512b9d19b7cb");
        }
    }
}
