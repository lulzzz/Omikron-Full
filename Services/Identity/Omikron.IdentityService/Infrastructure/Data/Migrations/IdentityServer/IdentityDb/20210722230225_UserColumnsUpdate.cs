using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class UserColumnsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "usm",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                schema: "usm",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Title",
                schema: "usm",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4895ffec55014fb284f97541b7c8841d");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "663efcf9b70549a5861cc2dc15b0ba0d");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c66dcfb1a9294616b010d91550e46673");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d109f5d3da3b47d7912bf66805e8d738");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(2081), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(2093) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4208), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4211) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4222), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4223) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4228), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4229) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4233), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4234) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4240), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4241) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4244), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4245) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4248), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4249) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4253), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4254) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4258), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4259) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4263), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4264) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4268), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4268) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4272), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4273) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4276), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4277) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4288), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4288) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4292), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4292) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4296), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4297) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4302), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4303) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 304, DateTimeKind.Utc).AddTicks(406), new DateTime(2021, 7, 22, 23, 2, 25, 304, DateTimeKind.Utc).AddTicks(417), new DateTime(2021, 7, 22, 23, 2, 25, 304, DateTimeKind.Utc).AddTicks(2357) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8b8fca96448b4e5c88ee203289305705");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "37d78e969dcf4508a062b329fd11a1b8");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2252384e44dd47c09113604e15c50cee");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "b9df22d423564666b1cf30fc6ebf4abc");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 649, DateTimeKind.Utc).AddTicks(9587), new DateTime(2021, 7, 22, 11, 41, 46, 649, DateTimeKind.Utc).AddTicks(9607) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2343), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2345) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2359), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2364), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2365) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2369), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2380), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2380) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2385), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2386) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2389), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2390) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2395), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2395) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2400), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2401) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2405), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2406) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2409), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2413), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2414) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2418), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2419) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2423), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2424) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2427), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2428) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2432), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2433) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2438), new DateTime(2021, 7, 22, 11, 41, 46, 650, DateTimeKind.Utc).AddTicks(2439) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 7, 22, 11, 41, 46, 659, DateTimeKind.Utc).AddTicks(8797), new DateTime(2021, 7, 22, 11, 41, 46, 659, DateTimeKind.Utc).AddTicks(8809), new DateTime(2021, 7, 22, 11, 41, 46, 660, DateTimeKind.Utc).AddTicks(1575) });
        }
    }
}
