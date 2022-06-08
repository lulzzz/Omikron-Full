using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddReportingScopes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ca596c9da25941ca9bb3060d8a58136e");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ff6cad4468c44563a898a3c424facb58");

            migrationBuilder.InsertData(
                schema: "usm",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Enabled", "ExternalId", "Name", "NormalizedName", "Type" },
                values: new object[] { 3, "50f5f5dfb5c14d338db4bcd8daf22153", "Reporting Management is a system role and represents the role with the ability to manage reports. This role cannot be deleted or edit since it's not a user-defined role.", true, new Guid("7bfc4797-714b-4fa2-b369-a5c73206800a"), "Reporting.Management", "REPORTING.MANAGEMENT", 1 });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 187, DateTimeKind.Utc).AddTicks(2074), new DateTime(2020, 6, 9, 14, 8, 47, 187, DateTimeKind.Utc).AddTicks(2135) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 187, DateTimeKind.Utc).AddTicks(9955), new DateTime(2020, 6, 9, 14, 8, 47, 187, DateTimeKind.Utc).AddTicks(9957) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(90), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(92) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(104), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(105) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(115), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(116) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(134), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(136) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(144), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(145) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(155), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(157) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(169), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(169) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(186), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(187) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(194), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(194) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(200), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(201) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(213), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(213) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(225), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(226) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(237), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(238) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(251), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(251) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(262), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(262) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(276), new DateTime(2020, 6, 9, 14, 8, 47, 188, DateTimeKind.Utc).AddTicks(278) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

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
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(3907), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7250), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7311), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7317), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7321), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7328), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7332), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7336), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7340), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7345), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7349), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7353), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7356), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7360), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7364), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7368), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7372), null });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2019, 9, 12, 20, 50, 9, 624, DateTimeKind.Utc).AddTicks(7440), null });
        }
    }
}
