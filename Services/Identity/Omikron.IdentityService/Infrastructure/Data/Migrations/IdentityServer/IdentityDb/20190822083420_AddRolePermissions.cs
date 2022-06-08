using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddRolePermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 11, new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8581), "Ability to see a page with a list of roles alongside with search functionality", 3, new Guid("42dc28e4-deca-41bf-960f-f63d4ad41e43"), null, "Search.Roles", "SEARCH.ROLES" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 12, new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8586), "Ability to see button for add role alongside with ability to create role", 3, new Guid("dd37cf7c-da69-4136-8d0b-96f2f40563c4"), null, "Add.Role", "ADD.ROLE" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 13, new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8589), "Ability to navigate to role details page and edit role", 3, new Guid("e4cc40f5-2100-497d-8b58-33667376a99a"), null, "Edit.Role", "EDIT.ROLE" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 14, new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8592), "Ability to see delete action alongside with ability to delete role", 3, new Guid("426d5137-c41c-4962-a1e0-3fa5c1dbbc45"), null, "Delete.Role", "DELETE.ROLE" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 15, new DateTime(2019, 8, 22, 8, 34, 19, 86, DateTimeKind.Utc).AddTicks(8595), "Ability to navigate to role details page without ability to edit role", 3, new Guid("0d6dac73-8728-4d9f-951f-ef4db97c996f"), null, "Read.Role", "READ.ROLE" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 11, 2 },
                    { 11, 1 },
                    { 12, 2 },
                    { 12, 1 },
                    { 13, 2 },
                    { 13, 1 },
                    { 14, 2 },
                    { 14, 1 },
                    { 15, 2 },
                    { 15, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 11, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 13, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 14, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 15, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4f3a890125de46f69c061a11a4d55f80");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9cc25f8be7a3410ebbb3ee2ce53593a0");

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
    }
}
