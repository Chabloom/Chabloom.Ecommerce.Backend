using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.Application
{
    public partial class ApplicationMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TenantHosts",
                columns: new[] { "Hostname", "TenantId" },
                values: new object[,]
                {
                    { "localhost", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") },
                    { "localhost:3003", new Guid("9cafce7f-d4a1-4874-b3c9-339836fd082c") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TenantHosts",
                keyColumn: "Hostname",
                keyValue: "localhost");

            migrationBuilder.DeleteData(
                table: "TenantHosts",
                keyColumn: "Hostname",
                keyValue: "localhost:3003");
        }
    }
}
