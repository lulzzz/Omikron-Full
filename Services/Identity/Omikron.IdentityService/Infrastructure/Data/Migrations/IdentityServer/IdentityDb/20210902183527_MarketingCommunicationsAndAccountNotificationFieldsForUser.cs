using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class MarketingCommunicationsAndAccountNotificationFieldsForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccountNotifications",
                schema: "usm",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MarketingCommunications",
                schema: "usm",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d42fbfe789224023ac6987c3700ebfbd");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ec51a9d562ed476fa99b94aee194b025");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ddd445f7c00140cf975ea84642943f27");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "fbf68c0f23d34f0aa1942d27cdaad91c");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 56, DateTimeKind.Utc).AddTicks(8595), new DateTime(2021, 9, 2, 18, 35, 27, 56, DateTimeKind.Utc).AddTicks(8608) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2130), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2146) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2173), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2174) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2181), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2182) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2189), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2190) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2204), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2205) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2212), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2213) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2218), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2219) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2225), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2226) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2239), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2241) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2269), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2271) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2286), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2287) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2294), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2295) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2303), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2304) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2312), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2313) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2401), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2403) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2410), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2411) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2424), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2426) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 70, DateTimeKind.Utc).AddTicks(2763), new DateTime(2021, 9, 2, 18, 35, 27, 70, DateTimeKind.Utc).AddTicks(2778), new DateTime(2021, 9, 2, 18, 35, 27, 70, DateTimeKind.Utc).AddTicks(4503) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNotifications",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MarketingCommunications",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3fd5f1624f4348988c87da52474a97f9");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "868cfee6041b4c1bb4ba3713f2cf2684");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f1af6f826b7641b190ba6d51ec7dbad2");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "1240dc9a2ea34205922dee4741752891");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(6570), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(6579) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8267), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8271) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8282), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8283) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8287), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8287) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8291), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8292) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8298), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8299) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8302), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8303) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8306), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8307) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8310), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8311) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8315), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8316) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8320), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8321) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8324), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8325) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8328), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8329) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8332), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8333) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8336), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8337) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8340), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8340) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8344), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8344) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8349), new DateTime(2021, 8, 18, 9, 42, 37, 336, DateTimeKind.Utc).AddTicks(8350) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 8, 18, 9, 42, 37, 346, DateTimeKind.Utc).AddTicks(2965), new DateTime(2021, 8, 18, 9, 42, 37, 346, DateTimeKind.Utc).AddTicks(2976), new DateTime(2021, 8, 18, 9, 42, 37, 346, DateTimeKind.Utc).AddTicks(4357) });
        }
    }
}
