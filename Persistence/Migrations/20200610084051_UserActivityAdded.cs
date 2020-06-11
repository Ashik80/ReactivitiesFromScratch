using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UserActivityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    AppUserId = table.Column<string>(nullable: false),
                    ActivityId = table.Column<Guid>(nullable: false),
                    DateJoined = table.Column<DateTime>(nullable: false),
                    IsHost = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => new { x.AppUserId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_UserActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserActivities_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_ActivityId",
                table: "UserActivities",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActivities");

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
        }
    }
}
