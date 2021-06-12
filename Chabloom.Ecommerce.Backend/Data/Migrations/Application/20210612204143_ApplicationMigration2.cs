using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.Application
{
    public partial class ApplicationMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FriendlyName = table.Column<string>(type: "text", nullable: true),
                    Xml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataProtectionKeys");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("30f42a18-8821-4913-b562-33d46d28f158"),
                column: "ConcurrencyStamp",
                value: "4970a2ee-0273-4dc6-9775-0bd0b03bae29");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f2183cb-401c-4d7c-9c3c-abc0e420f4f3"),
                column: "ConcurrencyStamp",
                value: "96309c79-49c7-470a-8c25-749cec10e97d");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"),
                column: "ConcurrencyStamp",
                value: "0c8bb047-f0a6-4523-8110-fd4fed52b4ae");

            migrationBuilder.UpdateData(
                table: "TenantRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6079515-7ed4-4bcf-b476-e747e31ebdbb"),
                column: "ConcurrencyStamp",
                value: "1000c177-6c40-4dff-a853-c36332b89186");
        }
    }
}
