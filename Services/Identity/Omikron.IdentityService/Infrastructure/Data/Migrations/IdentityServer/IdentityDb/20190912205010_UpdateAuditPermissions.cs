using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class UpdateAuditPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0e5c0f645cc34d0e82c0005327c14bfb");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "12d1fb1d5ba34e87b8d60414c890a9f9");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(3907));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7250));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7311));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7317));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7321));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7328));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7332));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7336));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7340));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7345));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7349));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7353));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7356));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7360));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7364));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7368));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7372));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "Domain" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7440), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2175b16d5f92415a89d7d55a99d20dac");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "62cbaff695fc481c9ebda266cbdbd7f4");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 174, DateTimeKind.Utc).AddTicks(9266));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2676));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2737));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2743));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2748));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2756));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2761));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2765));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2769));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2774));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2778));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2781));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2785));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2788));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2792));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2796));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2799));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "Domain" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2804), 4 });
        }
    }
}
