using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class PhoneNumberCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreationTime",
                schema: "usm",
                table: "PhoneNumbers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "TokenExpired",
                schema: "usm",
                table: "PhoneNumbers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c7d14210d59144208ceb96a602724ec3");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0496ef25e73f47a38b7b822e2d006d7f");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f387054956534cae94bec5c0e837a4cf");

            migrationBuilder.InsertData(
                schema: "usm",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Enabled", "ExternalId", "Name", "NormalizedName", "Type" },
                values: new object[] { 4, "f6dbaaa506df42878eaff5f05e3bd44a", "Omikron user is a default role for Omikron customers.", true, new Guid("fc6bd0e0-a573-41cd-87e1-c2be2ca27d7d"), "Omikron.User", "Omikron.USER", 2 });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(3906), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(3917) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6046), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6049) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6061), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6062) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6067), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6067) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6072), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6073) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6080), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6085), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6085) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6089), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6093), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6094) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6099), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6099) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6103), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6104) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6107), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6108) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6112), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6112) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6116), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6116) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6120), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6121) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6124), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6125) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6129), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6129) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6134), new DateTime(2021, 7, 21, 17, 33, 47, 967, DateTimeKind.Utc).AddTicks(6135) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "TokenCreationTime",
                schema: "usm",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "TokenExpired",
                schema: "usm",
                table: "PhoneNumbers");

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
    }
}
