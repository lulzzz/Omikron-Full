using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class InteractiveView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ecdc17c76f1141368fc248b8fcdf223b");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e1ebbcba0d30451e994f5e751ccd6646");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 111, DateTimeKind.Utc).AddTicks(9750));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3738));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3786));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3792));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3843));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3852));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3855));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3863));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3869));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3872));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3874));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3877));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3880));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3883));

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 16, new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3886), "Ability to run into interactive view in order to see required permissions for protected parts of the system", 3, new Guid("68e3c31c-aade-41d0-88ba-9048e7233b12"), null, "Interactive.View", "INTERACTIVE.VIEW" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 16, 2 });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 16, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 16, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a81c772270db4045Omikron8cd7b2d7f1d02");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6c96bc86beb74e649ca4e5bdb4561ebb");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(4496));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8510));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8549));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8555));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8558));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8566));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8569));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8572));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8575));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8581));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8581));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8586));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8589));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8592));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8595));
        }
    }
}
