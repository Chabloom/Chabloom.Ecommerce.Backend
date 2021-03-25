using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chabloom.Ecommerce.Backend.Data.Migrations.EcommerceDb
{
    public partial class EcommerceDbMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryId",
                table: "EcommerceProductCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "EcommerceProductCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_EcommerceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "EcommerceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantRoles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantRoleUsers",
                columns: table => new
                {
                    TenantRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantRoleUsers", x => new { x.TenantRoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TenantRoleUsers_TenantRoles_TenantRoleId",
                        column: x => x.TenantRoleId,
                        principalTable: "TenantRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantRoleUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("d4dc0126-c55d-474e-a44b-7e6d90822a59"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("5ab67841-f85c-416f-8a81-81b85bb7b219"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Joe's Tea Shop", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "EcommerceProductCategories",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Description", "Name", "ParentCategoryId", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "While black teas are made from the same Camellia sinensis plant as all teas, the oxidation and processing is what distinguishes black teas from the rest. Premium black teas are withered, rolled, oxidized and fired in an oven, creating a warm and toasty flavor. The lengthier oxidation process causes the tea leaves to develop into dark brown and black colors. The flavors can range from malty or smokey to fruity and sweet. Black teas range from mellow teas from China to full-bodied teas from Assam, India.", "Black", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "All green teas originate from the same species, the Camelia Sinensis. To make green tea, the fresh tea leaves are briefly cooked using either steam or dry heat. This process fixes the green colors and fresh flavors. The Chinese green teas are more mellow and smooth, while the Japanese green teas have the heft of rich, vegetal flavors, which comes from preservation of the chlorophyll.The general rule is that a cup of green tea contains about one-third as much caffeine as a cup of coffee. Green tea production methods vary but the focus is always to fix the green color. Thus, green teas are not oxidized.", "Green", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Herbal teas, also known as herbal infusions, are typically a blend of herbs, flowers, spices, and dried fruit. This blend of ingredients is then brewed in the same way as your favorite traditional tea, either loose or in tea sachets or bags. By combining quality ingredients, blends can be created that calm, invigorate, or treat minor ailments. Colors and flavors range from light and fruity to vibrant and spicy, to match your mood.", "Herbal", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "TenantRoles",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Name", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("830c7015-ab6c-4988-a603-ae3dc532d3b7"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("30f42a18-8821-4913-b562-33d46d28f158"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Manager", new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProductCategories",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "Description", "Name", "ParentCategoryId", "TenantId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Assam is India's largest producer of tea, and the broad flood plains make for some of the most fertile tea estates in the world.", "Assam", new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "The light and brisk First Flush teas focus your attention. While the darker teas of the later seasons are more mellow and often have wonderful flavors.", "Darjeeling", new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Powdered green teas have been consumed in China and Japan for centuries. However it is only in the last few decades that Westerners have acquired a taste for this ancient tea. We enjoy the bracing vegetal flavors, as well as the unusual process for preparing the tea.", "Matcha", new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new Guid("6a7e29dc-9eff-4f0d-bb14-51f63f142871"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProducts",
                columns: new[] { "Id", "CategoryId", "CreatedTimestamp", "CreatedUser", "Description", "Name", "Price", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "It seems like it is Yuzu’s time to shine. People are really liking this citrus fruit from Japan. So when we saw a blend of nice Sencha and Yuzu, we thought we should try it.", "Yuzu Sencha", 0m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "For beautiful Strawberry Kiwi Fruit Tea, we blend strawberries and dried fruit pieces with strawberry and kiwi flavors to create a vibrant ruby red drink. It looks festive brewed in a glass teapot, and tastes delicious hot or iced. ", "Strawberry Kiwi Fruit Tea", 0m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Blood Orange Fruit Tea, a brilliant blend of dried fruit, has the lovely and distinctive twist found in blood oranges. Delicious hot or cold, it brews an aromatic and vivid shade of orange.", "Blood Orange Fruit Tea", 0m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "EcommerceProducts",
                columns: new[] { "Id", "CategoryId", "CreatedTimestamp", "CreatedUser", "Description", "Name", "Price", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our Organic Assam is a rich, full leaf, medium bodied black tea. It has a slightly lighter liquor, with sweet honey flavor.", "Organic Assam", 0m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Our friends at Puttabong have done a great job with this tea. It is owned by Jayshree and is located north of Darjeeling town.  This tea came towards the end of the First Flush season in April. Brisk yet flavorful. Hats off!", "Puttabong 1st Flush Darjeeling", 0m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Matcha powdered green tea has been the pride of Uji for several centuries. This organic grade is great for everyday use.", "Organic Matcha", 0m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("5b605894-7618-4b97-a49b-9202f6e2799a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("745b7c71-03af-4ada-b18c-e370e5305e64"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6f054d9a-f2b9-49b0-86fa-71f328dd2daf"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("de6cac85-5c34-4ea6-b1d5-b1c8ec111070"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("9a467cbc-8128-44be-addd-b85f65f57cad"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedTimestamp", "CreatedUser", "ProductId", "UpdatedTimestamp", "UpdatedUser" },
                values: new object[] { new Guid("622d3d2c-1ff2-43b8-94e8-36ae7c2ca86b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceProductCategories_ParentCategoryId",
                table: "EcommerceProductCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EcommerceProductCategories_TenantId",
                table: "EcommerceProductCategories",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantRoles_TenantId",
                table: "TenantRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantRoleUsers_UserId",
                table: "TenantRoleUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_EcommerceProductCategories_EcommerceProductCategories_ParentCategoryId",
                table: "EcommerceProductCategories",
                column: "ParentCategoryId",
                principalTable: "EcommerceProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EcommerceProductCategories_Tenants_TenantId",
                table: "EcommerceProductCategories",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcommerceProductCategories_EcommerceProductCategories_ParentCategoryId",
                table: "EcommerceProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_EcommerceProductCategories_Tenants_TenantId",
                table: "EcommerceProductCategories");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "TenantRoleUsers");

            migrationBuilder.DropTable(
                name: "TenantRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_EcommerceProductCategories_ParentCategoryId",
                table: "EcommerceProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_EcommerceProductCategories_TenantId",
                table: "EcommerceProductCategories");

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("0321e99e-dd3b-402f-9cf6-e2ba284862d0"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("323565d2-3c93-4e05-81ff-ac745e22af9e"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("5e152dc1-203d-45e0-9eee-acc6f8bb74ee"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("78e540de-d2b3-4b1f-bb1e-988be3245088"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("cb949dda-57fb-4731-8379-b6f955b3102e"));

            migrationBuilder.DeleteData(
                table: "EcommerceProducts",
                keyColumn: "Id",
                keyValue: new Guid("ce3e245b-75c5-418e-98fe-3a115aa7395d"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("66272963-7577-4fb3-8cd6-a0bc411404e9"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("7d582944-4e2f-42ee-8a1e-199fd58762a6"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("b5e4f99f-227e-49be-a82f-8b4e06d35d96"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("cf059c18-ec58-4c6e-ae61-4dddabd61a6d"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("6470ca64-4d0a-4d94-8333-0f06d74e7ca1"));

            migrationBuilder.DeleteData(
                table: "EcommerceProductCategories",
                keyColumn: "Id",
                keyValue: new Guid("7b3a059d-4cda-46cc-890e-2c1f6451d6d6"));

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "EcommerceProductCategories");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EcommerceProductCategories");
        }
    }
}
