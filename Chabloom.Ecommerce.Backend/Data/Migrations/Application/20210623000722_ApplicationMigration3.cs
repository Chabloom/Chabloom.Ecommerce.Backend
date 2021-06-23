using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.Application
{
    public partial class ApplicationMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TenantHosts",
                columns: new[] { "Hostname", "TenantId" },
                values: new object[,]
                {
                    { "tea.dev-1.chabloom.com", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") },
                    { "tea.uat-1.chabloom.com", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871") },
                    { "ecommerce-dev-1.chabloom.com", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") },
                    { "ecommerce-uat-1.chabloom.com", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") },
                    { "greenjackets.dev-1.chabloom.com", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") },
                    { "greenjackets.uat-1.chabloom.com", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") }
                });

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("30f42a18-8821-4913-b562-33d46d28f158"),
                column: "ConcurrencyStamp",
                value: "4a9c08cb-d283-42f6-9fa0-face7d3606b9");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"),
                column: "ConcurrencyStamp",
                value: "ffb84d0f-9767-47ee-9e6a-0681709b8297");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"),
                column: "ConcurrencyStamp",
                value: "cd5950f5-abf4-436c-96f3-72ae9ef650d0");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"),
                column: "ConcurrencyStamp",
                value: "4e6eff0b-c1a8-4f89-b90f-397b47ee57d4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TenantHosts",
                keyColumn: "Hostname",
                keyValue: "ecommerce-dev-1.chabloom.com");

            migrationBuilder.DeleteData(
                table: "TenantHosts",
                keyColumn: "Hostname",
                keyValue: "ecommerce-uat-1.chabloom.com");

            migrationBuilder.DeleteData(
                table: "TenantHosts",
                keyColumn: "Hostname",
                keyValue: "greenjackets.dev-1.chabloom.com");

            migrationBuilder.DeleteData(
                table: "TenantHosts",
                keyColumn: "Hostname",
                keyValue: "greenjackets.uat-1.chabloom.com");

            migrationBuilder.DeleteData(
                table: "TenantHosts",
                keyColumn: "Hostname",
                keyValue: "tea.dev-1.chabloom.com");

            migrationBuilder.DeleteData(
                table: "TenantHosts",
                keyColumn: "Hostname",
                keyValue: "tea.uat-1.chabloom.com");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("30f42a18-8821-4913-b562-33d46d28f158"),
                column: "ConcurrencyStamp",
                value: "9d702594-3d47-401f-9bc4-c496ec3b1e6d");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"),
                column: "ConcurrencyStamp",
                value: "0626556e-e531-4693-9303-fbd10ffdfc74");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"),
                column: "ConcurrencyStamp",
                value: "e493026c-c0a8-4d79-9d1e-2ce87044d38e");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"),
                column: "ConcurrencyStamp",
                value: "6a2f578f-a42c-4895-af81-2d360279ef05");
        }
    }
}
