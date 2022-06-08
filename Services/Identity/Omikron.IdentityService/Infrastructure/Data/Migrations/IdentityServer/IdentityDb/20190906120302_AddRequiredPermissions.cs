using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddRequiredPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5f468a651cb5422ea18b1dead6c7ccd3");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c07bd08e0ed049e6814cdd014610a441");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(4798), "Ability to see a page with a list of tenants alongside with search functionality." });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8638), "Ability to see button for add tenant alongside with ability to create tenant. Required Permissions: Search.Tenants" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8683), "Ability to navigate to tenant details page and edit tenant. Required Permissions: Search.Tenants" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8689), "Ability to navigate to tenant details page without ability to edit tenant. Required Permissions: Search.Tenants" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8692), "Ability to see delete action alongside with ability to delete tenant. Required Permissions: Search.Tenants, Edit.Tenant" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8700), "Ability to see a page with a list of users alongside with search functionality." });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8703), "Ability to see button for add user alongside with ability to create user. Required Permissions: Search.Users" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8706), "Ability to navigate to user details page and edit user. Required Permissions: Search.Users" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8709), "Ability to see delete action alongside with ability to delete user. Required Permissions: Search.Users, Edit.User" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8714), "Ability to navigate to user details page without ability to edit user. Required Permissions: Search.Users" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8717), "Ability to see a page with a list of roles alongside with search functionality." });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8720), "Ability to see button for add role alongside with ability to create role. Required Permissions: Search.Roles" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8723), "Ability to navigate to role details page and edit role. Required Permissions: Search.Roles, Edit.Roles" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8726), "Ability to see delete action alongside with ability to delete role. Required Permissions: Search.Roles, Edit.Roles" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8731), "Ability to navigate to role details page without ability to edit role. Required Permissions: Search.Roles" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8734), "Ability to run into interactive view in order to see required permissions for protected parts of the system." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 111, DateTimeKind.Utc).AddTicks(9750), "Ability to see a page with a list of tenants alongside with search functionality" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3738), "Ability to see button for add tenant alongside with ability to create tenant" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3786), "Ability to navigate to tenant details page and edit tenant" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3792), "Ability to navigate to tenant details page without ability to edit tenant" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3843), "Ability to see delete action alongside with ability to delete tenant" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3852), "Ability to see a page with a list of users alongside with search functionality" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3855), "Ability to see button for add user alongside with ability to create user" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3860), "Ability to navigate to user details page and edit user" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3863), "Ability to see delete action alongside with ability to delete user" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3869), "Ability to navigate to user details page without ability to edit user" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3872), "Ability to see a page with a list of roles alongside with search functionality" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3874), "Ability to see button for add role alongside with ability to create role" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3877), "Ability to navigate to role details page and edit role" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3880), "Ability to see delete action alongside with ability to delete role" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3883), "Ability to navigate to role details page without ability to edit role" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2019, 8, 27, 8, 50, 30, 112, DateTimeKind.Utc).AddTicks(3886), "Ability to run into interactive view in order to see required permissions for protected parts of the system" });
        }
    }
}
