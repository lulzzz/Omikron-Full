using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class RemovedTenantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                schema: "usm",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_LoginProvider_ProviderKey_TenantId",
                schema: "usm",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "usm",
                table: "ConfirmationTokens");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "usm",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "usm",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "usm",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "usm",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "usm",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "usm",
                table: "AspNetRoleClaims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                schema: "usm",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b109ecd1c9844b74b90b1ea2554ce808");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "027ad99bc6014356958ffd1cb109d2ef");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4a84dfdea2284e139834a5a50452bf16");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "ac65b727f1db4bfa9aa5c8e7d48f59ad");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(6305), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(6318) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7823), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7825) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7834), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7834) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7838), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7839) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7843), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7844) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7852), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7852) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7856), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7857) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7860), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7861) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7864), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7865) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7869), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7870) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7874), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7874) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7878), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7879) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7882), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7883) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7886), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7887) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7890), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7891) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7894), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7894) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7898), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7903), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7904) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 213, DateTimeKind.Utc).AddTicks(667), new DateTime(2021, 7, 24, 5, 44, 8, 213, DateTimeKind.Utc).AddTicks(678), new DateTime(2021, 7, 24, 5, 44, 8, 213, DateTimeKind.Utc).AddTicks(1776) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                schema: "usm",
                table: "AspNetUserLogins");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "usm",
                table: "ConfirmationTokens",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "usm",
                table: "AspNetUserTokens",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "usm",
                table: "AspNetUserRoles",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "usm",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "usm",
                table: "AspNetUserLogins",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "usm",
                table: "AspNetUserClaims",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "usm",
                table: "AspNetRoleClaims",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                schema: "usm",
                table: "AspNetUserLogins",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4895ffec55014fb284f97541b7c8841d");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "663efcf9b70549a5861cc2dc15b0ba0d");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c66dcfb1a9294616b010d91550e46673");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d109f5d3da3b47d7912bf66805e8d738");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(2081), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(2093) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4208), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4211) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4222), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4223) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4228), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4229) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4233), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4234) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4240), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4241) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4244), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4245) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4248), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4249) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4253), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4254) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4258), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4259) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4263), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4264) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4268), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4268) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4272), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4273) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4276), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4277) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4288), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4288) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4292), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4292) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4296), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4297) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4302), new DateTime(2021, 7, 22, 23, 2, 25, 296, DateTimeKind.Utc).AddTicks(4303) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 7, 22, 23, 2, 25, 304, DateTimeKind.Utc).AddTicks(406), new DateTime(2021, 7, 22, 23, 2, 25, 304, DateTimeKind.Utc).AddTicks(417), new DateTime(2021, 7, 22, 23, 2, 25, 304, DateTimeKind.Utc).AddTicks(2357) });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_LoginProvider_ProviderKey_TenantId",
                schema: "usm",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey", "TenantId" },
                unique: true);
        }
    }
}
