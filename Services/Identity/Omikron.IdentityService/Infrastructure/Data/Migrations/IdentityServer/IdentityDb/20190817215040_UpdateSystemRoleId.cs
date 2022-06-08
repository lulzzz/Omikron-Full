using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class UpdateSystemRoleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "4f3a890125de46f69c061a11a4d55f80", new Guid("dffa36f1-df2a-419d-9387-a32509a14d69") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "9cc25f8be7a3410ebbb3ee2ce53593a0", new Guid("cb6165cb-4cdf-4fdc-9951-71a9a0485fb9") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(2051));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(6108));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(6173));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(6179));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(6184));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(6194));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(6202));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(6206));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 21, 50, 39, 207, DateTimeKind.Utc).AddTicks(6212));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "8fb7473c0a2b45a28bebba4f45cb04df", new Guid("df01d172-224f-4dbd-821d-84af5dfd26ba") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "b4afb238bd76474a9e445faf21e7dcb6", new Guid("5c2710ad-1bbd-4f56-9ed3-88fcd321f13a") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(1976));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5838));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5896));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5903));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5908));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5916));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5920));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5923));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5968));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5974));
        }
    }
}
