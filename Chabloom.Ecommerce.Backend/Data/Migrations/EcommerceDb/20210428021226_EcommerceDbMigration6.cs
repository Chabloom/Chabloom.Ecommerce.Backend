using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00"),
                column: "Description",
                value: "Designed to be a great tasting water, our water is filtered by reverse osmosis to remove impurities, then enhanced with a special blend of minerals for a pure, crisp, fresh taste.");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("781d9646-1156-4cbb-a581-329f2ae34744"),
                column: "Description",
                value: "The original burger starts with a 100% pure beef burger seasoned with just a pinch of salt and pepper. Then, the burger is topped with a tangy pickle, chopped onions, ketchup and mustard.");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969"),
                column: "Description",
                value: "Bud Light is a premium beer with incredible drinkability that has made it a top selling American beer that everybody knows and loves. This light beer is brewed using a combination of barley malts, rice and a blend of premium aroma hop varieties. Featuring a fresh, clean taste with subtle hop aromas, this light lager delivers ultimate refreshment with its delicate malt sweetness and crisp finish.");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7"),
                column: "Description",
                value: "Our tasty all beef hot dogs are all natural, skinless, uncured and made with beef that’s never given antibiotics.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("0aecbbc7-0e6c-4727-bc05-9d3700397b00"),
                column: "Description",
                value: "Water");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("781d9646-1156-4cbb-a581-329f2ae34744"),
                column: "Description",
                value: "Hamburger");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("9aa49ae2-53bb-417a-b1f7-1bd9f6578969"),
                column: "Description",
                value: "Beer");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("c615100b-e2d9-48a4-81c1-824a3bb12cb7"),
                column: "Description",
                value: "Hot dog");
        }
    }
}
