using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class FixRolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "3df9062768f541cf93e011896d8fc858", new Guid("e99265aa-8af6-4528-9868-5c6ab83bcdcb") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "d30c3a50dd81455a9d2edc7b19641b16", new Guid("b65bafa9-e580-44fd-ac89-ee8885a77042") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(3857));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(7125));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(7298));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(7304));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(7309));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(7317));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(7321));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(7324));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(7327));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 31, 45, 526, DateTimeKind.Utc).AddTicks(7332));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "aad7b7be72bf42ea8f994c2d460088ae", new Guid("05a075a5-076a-465c-811d-0f5c875e9b3f") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "b953e152e59a40348608d82792882b5d", new Guid("4e313f82-5801-41ec-b147-86697f85fccf") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(1143));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4366));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4512));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4518));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4522));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4528));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4532));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4535));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4540));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4545));
        }
    }
}
