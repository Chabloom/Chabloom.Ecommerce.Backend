using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcommerceOrders_EcommerceUsers_UserId",
                table: "EcommerceOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "EcommerceOrders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_EcommerceOrders_EcommerceUsers_UserId",
                table: "EcommerceOrders",
                column: "UserId",
                principalTable: "EcommerceUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcommerceOrders_EcommerceUsers_UserId",
                table: "EcommerceOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "EcommerceOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EcommerceOrders_EcommerceUsers_UserId",
                table: "EcommerceOrders",
                column: "UserId",
                principalTable: "EcommerceUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
