using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"),
                column: "Price",
                value: 2.99m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"),
                column: "Price",
                value: 2.99m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"),
                column: "Price",
                value: 3.99m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"),
                column: "Price",
                value: 4.99m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"),
                column: "Price",
                value: 2.99m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"),
                column: "Price",
                value: 2.99m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"),
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"),
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"),
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"),
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"),
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"),
                column: "Price",
                value: 0m);
        }
    }
}
