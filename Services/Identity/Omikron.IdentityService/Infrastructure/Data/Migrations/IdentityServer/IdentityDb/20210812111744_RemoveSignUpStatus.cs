using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class RemoveSignUpStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignUpStatus",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IdentityTokenAvailable",
                schema: "usm",
                table: "PhoneNumbers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9d8a2aaaf89f474db9a3cb1dad800afb");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0ac37a7ca00743138e437af2cb4f1d7e");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1d948abc9ae94aa8bdbe23f0bf85953e");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "3459a68dde484268bfe31c364a94e4dc");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(5441), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8527), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8531) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8544), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8545) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8550), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8551) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8555), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8556) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8564), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8565) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8568), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8569) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8633), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8634) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8638), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8639) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8644), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8645) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8648), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8649) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8652), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8653) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8657), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8657) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8660), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8661) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8664), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8665) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8668), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8669) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8673), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8674) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8679), new DateTime(2021, 8, 12, 11, 17, 44, 269, DateTimeKind.Utc).AddTicks(8680) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 8, 12, 11, 17, 44, 280, DateTimeKind.Utc).AddTicks(4209), new DateTime(2021, 8, 12, 11, 17, 44, 280, DateTimeKind.Utc).AddTicks(4219), new DateTime(2021, 8, 12, 11, 17, 44, 280, DateTimeKind.Utc).AddTicks(5814) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityTokenAvailable",
                schema: "usm",
                table: "PhoneNumbers");

            migrationBuilder.AddColumn<int>(
                name: "SignUpStatus",
                schema: "usm",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "088200de69864fe882a287dd1e5a76b5");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ac8f7abdf8d742f0b79421373b672a8e");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "43a9188b899e4509b9b727f5e45a8a65");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "5f14f30035df443dad26a91bf5ff9ece");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(425), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(436) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2573), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2576) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2590), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2591) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2596), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2597) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2601), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2602) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2610), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2611) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2614), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2615) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2619), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2623), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2624) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2629), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2633), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2634) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2637), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2638) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2642), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2642) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2646), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2647) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2650), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2651) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2654), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2655) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2659), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2665), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2666) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 142, DateTimeKind.Utc).AddTicks(6344), new DateTime(2021, 8, 10, 23, 3, 36, 142, DateTimeKind.Utc).AddTicks(6360), new DateTime(2021, 8, 10, 23, 3, 36, 142, DateTimeKind.Utc).AddTicks(9016) });
        }
    }
}
