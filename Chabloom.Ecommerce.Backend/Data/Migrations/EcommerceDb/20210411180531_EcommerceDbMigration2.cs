using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcommerceUsers_EcommerceRoles_RoleId",
                table: "EcommerceUsers");

            migrationBuilder.DropIndex(
                name: "IX_EcommerceUsers_RoleId",
                table: "EcommerceUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EcommerceRoles",
                table: "EcommerceRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EcommerceRoles");

            migrationBuilder.DropColumn(
                name: "CreatedTimestamp",
                table: "EcommerceRoles");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "EcommerceRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedTimestamp",
                table: "EcommerceRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedUser",
                table: "EcommerceRoles");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "EcommerceUsers",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "EcommerceProductImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PickupMethodName",
                table: "EcommerceOrders",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EcommerceRoles",
                table: "EcommerceRoles",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "EcommercePickupMethods",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommercePickupMethods", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "EcommerceProductPickupMethods",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PickupMethodName = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcommerceProductPickupMethods", x => new { x.ProductId, x.PickupMethodName });
                    table.ForeignKey(
                        name: "FK_EcommerceProductPickupMethods_EcommercePickupMethods_PickupMethodName",
                        column: x => x.PickupMethodName,
                        principalTable: "EcommercePickupMethods",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcommerceProductPickupMethods_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EcommercePickupMethods",
                column: "Name",
                values: new object[]
                {
                    "In-Store",
                    "Pickup",
                    "Delivery",
                    "Shipping"
                });

            migrationBuilder.UpdateData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("5b605894-7618-4b97-a49b-9202f6e2799a"),
                column: "Filename",
                value: "CB949DDA-57FB-4731-8379-B6F955B3102E.webp");

            migrationBuilder.UpdateData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("622d3d2c-1ff2-43b8-94e8-36ae7c2ca86b"),
                column: "Filename",
                value: "5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE.webp");

            migrationBuilder.UpdateData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("6f054d9a-f2b9-49b0-86fa-71f328dd2daf"),
                column: "Filename",
                value: "0321E99E-DD3B-402F-9CF6-E2BA284862D0.webp");

            migrationBuilder.UpdateData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("745b7c71-03af-4ada-b18c-e370e5305e64"),
                column: "Filename",
                value: "CE3E245B-75C5-418E-98FE-3A115AA7395D.webp");

            migrationBuilder.UpdateData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("9a467cbc-8128-44be-addd-b85f65f57cad"),
                column: "Filename",
                value: "78E540DE-D2B3-4B1F-BB1E-988BE3245088.webp");

            migrationBuilder.UpdateData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("de6cac85-5c34-4ea6-b1d5-b1c8ec111070"),
                column: "Filename",
                value: "323565D2-3C93-4E05-81FF-AC745E22AF9E.webp");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"),
                column: "Name",
                value: "Blood Orange Fruit Tea (25 tea bags)");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"),
                column: "Name",
                value: "Organic Assam (25 tea bags)");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"),
                column: "Name",
                value: "Organic Matcha (25 tea bags)");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"),
                column: "Name",
                value: "Puttabong 1st Flush Darjeeling (25 tea bags)");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"),
                column: "Name",
                value: "Yuzu Sencha (25 tea bags)");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"),
                column: "Name",
                value: "Strawberry Kiwi Fruit Tea (25 tea bags)");

            migrationBuilder.InsertData(
                table: "EcommerceProducts",
                columns: new[] { "Id", "CategoryId", "CreatedTimestamp", "CreatedUser", "Description", "Name", "Price", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("e95543da-bb67-4859-8b98-d92041d58d8d"), new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Matcha powdered green tea has been the pride of Uji for several centuries. This organic grade is great for everyday use.", "Organic Matcha (cup)", 3.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It looks festive brewed in a glass teapot, and tastes delicious hot or iced. ", "Strawberry Kiwi Fruit Tea (cup)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and distinctive twist found in blood oranges. Delicious hot or cold, it brews an aromatic and vivid shade of orange.", "Blood Orange Fruit Tea (cup)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9"), new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our friends at Puttabong have done a great job with this tea. It is owned by Jayshree and is located north of Darjeeling town.  This tea came towards the end of the First Flush season in April. Brisk yet flavorful. Hats off!", "Puttabong 1st Flush Darjeeling (cup)", 4.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("41e5e396-6757-42f3-9149-3b084976545a"), new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly lighter liquor, with sweet honey flavor.", "Organic Assam (cup)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a"), new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "It seems like it is Yuzu’s time to shine. People are really liking this citrus fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we should try it.", "Yuzu Sencha (cup)", 2.99m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Filename", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("ee465f84-413c-41f9-a3da-a149f241bdfa"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "CE3E245B-75C5-418E-98FE-3A115AA7395D.webp", new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0aabec17-aaa1-4f35-9b27-9b199a2aa67c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "5E152DC1-203D-45E0-9EEE-ACC6F8BB74EE.webp", new Guid("e95543da-bb67-4859-8b98-d92041d58d8d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a030e5d7-45a2-407c-ac56-b7f310542fb9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "CB949DDA-57FB-4731-8379-B6F955B3102E.webp", new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0d7936d7-0d94-4f31-9314-135ea1daf3c9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "78E540DE-D2B3-4B1F-BB1E-988BE3245088.webp", new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ef15fab8-2ce0-4bbf-a8e1-d8e1b541ce6a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "323565D2-3C93-4E05-81FF-AC745E22AF9E.webp", new Guid("41e5e396-6757-42f3-9149-3b084976545a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c1779df8-8cc8-425d-9fea-dca8531fd228"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "0321E99E-DD3B-402F-9CF6-E2BA284862D0.webp", new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductPickupMethods",
                columns: new[] { "PickupMethodName", "ProductId" },
                values: new object[,]
                {
                    { "Pickup", new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e") },
                    { "In-Store", new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3") },
                    { "In-Store", new Guid("e95543da-bb67-4859-8b98-d92041d58d8d") },
                    { "In-Store", new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a") },
                    { "In-Store", new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9") },
                    { "In-Store", new Guid("41e5e396-6757-42f3-9149-3b084976545a") },
                    { "Shipping", new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0") },
                    { "Shipping", new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee") },
                    { "Shipping", new Guid("cb949dda-57fb-4731-8379-b6f955b3102e") },
                    { "Shipping", new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088") },
                    { "Shipping", new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e") },
                    { "Pickup", new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0") },
                    { "Pickup", new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d") },
                    { "Pickup", new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee") },
                    { "Pickup", new Guid("cb949dda-57fb-4731-8379-b6f955b3102e") },
                    { "Pickup", new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088") },
                    { "Shipping", new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d") },
                    { "In-Store", new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceUsers_RoleName",
                table: "EcommerceUsers",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceOrders_PickupMethodName",
                table: "EcommerceOrders",
                column: "PickupMethodName");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceProductPickupMethods_PickupMethodName",
                table: "EcommerceProductPickupMethods",
                column: "PickupMethodName");

            migrationBuilder.AddForeignKey(
                name: "FK_EcommerceOrders_EcommercePickupMethods_PickupMethodName",
                table: "EcommerceOrders",
                column: "PickupMethodName",
                principalTable: "EcommercePickupMethods",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EcommerceUsers_EcommerceRoles_RoleName",
                table: "EcommerceUsers",
                column: "RoleName",
                principalTable: "EcommerceRoles",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcommerceOrders_EcommercePickupMethods_PickupMethodName",
                table: "EcommerceOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_EcommerceUsers_EcommerceRoles_RoleName",
                table: "EcommerceUsers");

            migrationBuilder.DropTable(
                name: "EcommerceProductPickupMethods");

            migrationBuilder.DropTable(
                name: "EcommercePickupMethods");

            migrationBuilder.DropIndex(
                name: "IX_EcommerceUsers_RoleName",
                table: "EcommerceUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EcommerceRoles",
                table: "EcommerceRoles");

            migrationBuilder.DropIndex(
                name: "IX_EcommerceOrders_PickupMethodName",
                table: "EcommerceOrders");

            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("0aabec17-aaa1-4f35-9b27-9b199a2aa67c"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("0d7936d7-0d94-4f31-9314-135ea1daf3c9"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("a030e5d7-45a2-407c-ac56-b7f310542fb9"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("c1779df8-8cc8-425d-9fea-dca8531fd228"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("ee465f84-413c-41f9-a3da-a149f241bdfa"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductImages",
                keyColumn: "Id",
                keyValue: new Guid("ef15fab8-2ce0-4bbf-a8e1-d8e1b541ce6a"));

            migrationBuilder.DeleteData(
                table: "EcommerceRoles",
                keyColumn: "Name",
                keyValue: "Admin");

            migrationBuilder.DeleteData(
                table: "EcommerceRoles",
                keyColumn: "Name",
                keyValue: "Manager");

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("0418ae94-b020-4b2a-9697-7ddcbe2bd72a"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("2446bd16-df0a-4f7e-9e23-18cb1d5d008e"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("41e5e396-6757-42f3-9149-3b084976545a"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("4d0bcd02-9dab-499e-92cd-8ba9f252b2a9"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("728617aa-ecd3-48ac-bdad-ccf660f775a3"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("e95543da-bb67-4859-8b98-d92041d58d8d"));

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "EcommerceUsers");

            migrationBuilder.DropColumn(
                name: "Filename",
                table: "EcommerceProductImages");

            migrationBuilder.DropColumn(
                name: "PickupMethodName",
                table: "EcommerceOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "EcommerceRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTimestamp",
                table: "EcommerceRoles",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUser",
                table: "EcommerceRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedTimestamp",
                table: "EcommerceRoles",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUser",
                table: "EcommerceRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EcommerceRoles",
                table: "EcommerceRoles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"),
                column: "Name",
                value: "Blood Orange Fruit Tea");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"),
                column: "Name",
                value: "Organic Assam");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"),
                column: "Name",
                value: "Organic Matcha");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"),
                column: "Name",
                value: "Puttabong 1st Flush Darjeeling");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"),
                column: "Name",
                value: "Yuzu Sencha");

            migrationBuilder.UpdateData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"),
                column: "Name",
                value: "Strawberry Kiwi Fruit Tea");

            migrationBuilder.InsertData(
                table: "EcommerceRoles",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("d4dc0126-c55d-474e-a44b-7e6d90822a59"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5ab67841-f85c-416f-8a81-81b85bb7b219"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceUsers_RoleId",
                table: "EcommerceUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_EcommerceUsers_EcommerceRoles_RoleId",
                table: "EcommerceUsers",
                column: "RoleId",
                principalTable: "EcommerceRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
