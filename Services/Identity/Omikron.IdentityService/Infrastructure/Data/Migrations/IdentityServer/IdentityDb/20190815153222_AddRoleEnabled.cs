using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddRoleEnabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "usm",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Enabled", "ExternalId" },
                values: new object[] { "7cfeee7f919e43c8b96be30f84750c15", true, new Guid("fb7695d6-8e21-47c6-94bf-adce79b03661") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Enabled", "ExternalId" },
                values: new object[] { "21467a1a611a47f0a8c18fafa42a5b5d", true, new Guid("7ee68631-fc21-486a-b0f5-d866ff1bd2d7") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(3219));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7247));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7252));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7255));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7261));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7264));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7267));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7269));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7275));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "usm",
                table: "AspNetRoles");

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
    }
}
