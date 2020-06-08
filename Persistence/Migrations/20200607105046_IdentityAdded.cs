using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class IdentityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("0b5c031d-00e4-4728-a9ec-b6506e07570e"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("13e3b4c2-b880-4aa8-8e0c-33f4a5b45303"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("1876c616-63fc-4715-9c16-b0c86cdc360c"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("23ead8f4-a6d5-421c-b872-2eb8530c2c67"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("4080650d-c859-431e-9138-7e5f10f16036"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("4a3948f8-8333-47c6-adcf-680c62de2fbd"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("8a77952c-0c13-4b97-8b89-c548d8ba56e2"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("be3b1404-7aa9-4508-ac3c-229fb307b27e"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("d3b8642e-2c30-4c68-ab44-496e447a24dc"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("eba2380c-2937-4749-8538-56391e3c7b4d"));

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Category", "City", "Date", "Description", "Title", "Venue" },
                values: new object[,]
                {
                    { new Guid("f44eaf90-2468-4d21-ba86-23b15ab978b8"), "drinks", "London", new DateTime(2020, 4, 7, 16, 50, 45, 732, DateTimeKind.Local).AddTicks(302), "Activity 2 months ago", "Past Activity 1", "Pub" },
                    { new Guid("819dad26-b441-4522-ab8c-53d172cb6f3b"), "culture", "Paris", new DateTime(2020, 5, 7, 16, 50, 45, 733, DateTimeKind.Local).AddTicks(2098), "Activity 1 month ago", "Past Activity 2", "Louvre" },
                    { new Guid("60c1d57d-e28f-4f1c-9734-d593eca0a741"), "culture", "London", new DateTime(2020, 7, 7, 16, 50, 45, 733, DateTimeKind.Local).AddTicks(2170), "Activity 1 month in future", "Future Activity 1", "Natural History Museum" },
                    { new Guid("a64f3925-5f7f-479e-9261-bc879aa7edd0"), "music", "London", new DateTime(2020, 8, 7, 16, 50, 45, 733, DateTimeKind.Local).AddTicks(2176), "Activity 2 months in future", "Future Activity 2", "O2 Arena" },
                    { new Guid("bfbd24ad-111a-4081-8bca-59b0cf87e22f"), "drinks", "London", new DateTime(2020, 9, 7, 16, 50, 45, 733, DateTimeKind.Local).AddTicks(2181), "Activity 3 months in future", "Future Activity 3", "Another pub" },
                    { new Guid("7b9a56d3-960e-497d-a048-fc660c832dbf"), "drinks", "London", new DateTime(2020, 10, 7, 16, 50, 45, 733, DateTimeKind.Local).AddTicks(2197), "Activity 4 months in future", "Future Activity 4", "Yet another pub" },
                    { new Guid("29ff8c0a-3a11-4572-99c7-60ef116418ba"), "drinks", "London", new DateTime(2020, 11, 7, 16, 50, 45, 733, DateTimeKind.Local).AddTicks(2201), "Activity 5 months in future", "Future Activity 5", "Just another pub" },
                    { new Guid("a76e1831-b10c-4c93-a481-fc5cee20e6ed"), "music", "London", new DateTime(2020, 12, 7, 16, 50, 45, 733, DateTimeKind.Local).AddTicks(2205), "Activity 6 months in future", "Future Activity 6", "Roundhouse Camden" },
                    { new Guid("a56766a5-2108-4769-97fd-c4aabdf74925"), "travel", "London", new DateTime(2021, 1, 7, 16, 50, 45, 733, DateTimeKind.Local).AddTicks(2208), "Activity 2 months ago", "Future Activity 7", "Somewhere on the Thames" },
                    { new Guid("661d9f00-3efd-4427-b6b6-b9d7df92f43e"), "film", "London", new DateTime(2021, 2, 7, 16, 50, 45, 733, DateTimeKind.Local).AddTicks(2213), "Activity 8 months in future", "Future Activity 8", "Cinema" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("29ff8c0a-3a11-4572-99c7-60ef116418ba"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("60c1d57d-e28f-4f1c-9734-d593eca0a741"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("661d9f00-3efd-4427-b6b6-b9d7df92f43e"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("7b9a56d3-960e-497d-a048-fc660c832dbf"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("819dad26-b441-4522-ab8c-53d172cb6f3b"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("a56766a5-2108-4769-97fd-c4aabdf74925"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("a64f3925-5f7f-479e-9261-bc879aa7edd0"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("a76e1831-b10c-4c93-a481-fc5cee20e6ed"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("bfbd24ad-111a-4081-8bca-59b0cf87e22f"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("f44eaf90-2468-4d21-ba86-23b15ab978b8"));

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Category", "City", "Date", "Description", "Title", "Venue" },
                values: new object[,]
                {
                    { new Guid("13e3b4c2-b880-4aa8-8e0c-33f4a5b45303"), "drinks", "London", new DateTime(2020, 4, 1, 14, 54, 20, 668, DateTimeKind.Local).AddTicks(7647), "Activity 2 months ago", "Past Activity 1", "Pub" },
                    { new Guid("0b5c031d-00e4-4728-a9ec-b6506e07570e"), "culture", "Paris", new DateTime(2020, 5, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8341), "Activity 1 month ago", "Past Activity 2", "Louvre" },
                    { new Guid("4a3948f8-8333-47c6-adcf-680c62de2fbd"), "culture", "London", new DateTime(2020, 7, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8420), "Activity 1 month in future", "Future Activity 1", "Natural History Museum" },
                    { new Guid("eba2380c-2937-4749-8538-56391e3c7b4d"), "music", "London", new DateTime(2020, 8, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8425), "Activity 2 months in future", "Future Activity 2", "O2 Arena" },
                    { new Guid("23ead8f4-a6d5-421c-b872-2eb8530c2c67"), "drinks", "London", new DateTime(2020, 9, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8429), "Activity 3 months in future", "Future Activity 3", "Another pub" },
                    { new Guid("8a77952c-0c13-4b97-8b89-c548d8ba56e2"), "drinks", "London", new DateTime(2020, 10, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8432), "Activity 4 months in future", "Future Activity 4", "Yet another pub" },
                    { new Guid("be3b1404-7aa9-4508-ac3c-229fb307b27e"), "drinks", "London", new DateTime(2020, 11, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8448), "Activity 5 months in future", "Future Activity 5", "Just another pub" },
                    { new Guid("4080650d-c859-431e-9138-7e5f10f16036"), "music", "London", new DateTime(2020, 12, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8452), "Activity 6 months in future", "Future Activity 6", "Roundhouse Camden" },
                    { new Guid("d3b8642e-2c30-4c68-ab44-496e447a24dc"), "travel", "London", new DateTime(2021, 1, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8455), "Activity 2 months ago", "Future Activity 7", "Somewhere on the Thames" },
                    { new Guid("1876c616-63fc-4715-9c16-b0c86cdc360c"), "film", "London", new DateTime(2021, 2, 1, 14, 54, 20, 669, DateTimeKind.Local).AddTicks(8459), "Activity 8 months in future", "Future Activity 8", "Cinema" }
                });
        }
    }
}
