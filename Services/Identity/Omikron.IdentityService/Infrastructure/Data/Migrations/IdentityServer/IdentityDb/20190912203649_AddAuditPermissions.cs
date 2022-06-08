using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddAuditPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2175b16d5f92415a89d7d55a99d20dac");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "62cbaff695fc481c9ebda266cbdbd7f4");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 174, DateTimeKind.Utc).AddTicks(9266));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2676));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2737));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2743));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2748));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2756));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2761));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2765));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2769));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2774));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2778));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2781));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2785));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2788));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2792));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2796));

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 17, new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2799), "Ability to trace data for particular tenant. The permission includes also the ability to see data content what was changed.", 4, new Guid("77fadd97-22d3-489e-882e-b106f6805bdf"), null, "Search.DataChangeLogs", "SEARCH.DATACHANGELOGS" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 18, new DateTime(2019, 9, 12, 20, 36, 48, 175, DateTimeKind.Utc).AddTicks(2804), "Ability to trace data for all tenants. The permission includes also the ability to see data content what was changed.", 4, new Guid("c5b037e8-1df3-4bc0-92fd-b1cd5c1a1d17"), null, "Search.DataChangeLogs.ByTenant", "SEARCH.DATACHANGELOGS.BYTENANT" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 17, 2 });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 17, 1 });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 18, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 17, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 18, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18);

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
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(4798));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8638));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8683));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8692));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8700));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8703));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8706));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8709));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8714));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8717));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8720));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8723));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8726));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8731));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2019, 9, 6, 12, 3, 0, 72, DateTimeKind.Utc).AddTicks(8734));
        }
    }
}
