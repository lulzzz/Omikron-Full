using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class UserFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d526864038d1466e8e84a6387ac28d7e");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bb32701f1db14eebb1653456459cddcd");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4597e66ddab14e528bc133111bad984d");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 500, DateTimeKind.Utc).AddTicks(8669), new DateTime(2021, 7, 21, 12, 29, 26, 500, DateTimeKind.Utc).AddTicks(8678) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1603), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1607) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1622), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1623) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1628), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1628) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1633), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1634) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1642), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1643) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1647), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1648) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1653), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1654) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1657), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1658) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1663), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1664) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1677), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1677) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1681), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1682) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1686), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1687) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1691), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1692) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1695), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1696) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1700), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1701) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1705), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1706) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1711), new DateTime(2021, 7, 21, 12, 29, 26, 501, DateTimeKind.Utc).AddTicks(1712) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b84c88f79f164a7eaebf9f42dbdf8cfe");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "086b538f96b84c64b368374eb4f52859");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1c99bfa0876b4dc4b1333a99a21865b0");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(3678), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(3691) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5720), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5723) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5736), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5737) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5742), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5743) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5747), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5748) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5755), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5756) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5759), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5764), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5765) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5769), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5775), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5776) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5779), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5784), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5785) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5788), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5789) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5793), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5794) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5797), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5798) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5801), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5802) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5807), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5807) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5812), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5813) });
        }
    }
}
