using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddPermissionsTenantAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "usm",
                table: "AspNetRoles",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "usm",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Description", "ExternalId", "NormalizedName", "Type" },
                values: new object[] { "8fb7473c0a2b45a28bebba4f45cb04df", "Tenant Administrator Role is a system role and represents the role with the ability to managing users and roles for the tenant. This role cannot be deleted or edit since it's not a user-defined role.", new Guid("df01d172-224f-4dbd-821d-84af5dfd26ba"), "TENANT.ADMINISTRATOR", 1 });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Description", "ExternalId", "NormalizedName", "Type" },
                values: new object[] { "b4afb238bd76474a9e445faf21e7dcb6", "System Tenant Administrator Role is a system role and represents the role with the ability to managing tenants users and roles for the main tenant. This role cannot be deleted or edit since it's not a user-defined role.", new Guid("5c2710ad-1bbd-4f56-9ed3-88fcd321f13a"), "SYSTEM.TENANT.ADMINISTRATOR", 1 });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(1976), "SEARCH.TENANTS" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5838), "ADD.TENANT" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5896), "EDIT.TENANT" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5903), "READ.TENANT" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5908), "DELETE.TENANT" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5916), "SEARCH.USERS" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5920), "ADD.USER" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5923), "EDIT.USER" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5968), "DELETE.USER" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 17, 11, 22, 18, 156, DateTimeKind.Utc).AddTicks(5974), "READ.USER" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 9, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 10, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "usm",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "usm",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId", "NormalizedName" },
                values: new object[] { "7cfeee7f919e43c8b96be30f84750c15", new Guid("fb7695d6-8e21-47c6-94bf-adce79b03661"), "Tenant.Administrator" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "ExternalId", "NormalizedName" },
                values: new object[] { "21467a1a611a47f0a8c18fafa42a5b5d", new Guid("7ee68631-fc21-486a-b0f5-d866ff1bd2d7"), "System.Tenant.Administrator" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(3219), "Search.Tenants" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7110), "Add.Tenant" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7247), "Edit.Tenant" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7252), "Read.Tenant" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7255), "Delete.Tenant" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7261), "Search.Users" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7264), "Add.User" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7267), "Edit.User" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7269), "Delete.User" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "NormalizedName" },
                values: new object[] { new DateTime(2019, 8, 15, 15, 32, 19, 440, DateTimeKind.Utc).AddTicks(7275), "Read.User" });
        }
    }
}
