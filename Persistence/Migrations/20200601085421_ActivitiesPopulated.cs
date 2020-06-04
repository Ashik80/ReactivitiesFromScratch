using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ActivitiesPopulated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
