using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddExternalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                schema: "usm",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                schema: "usm",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "1ad196190af0424bb3058c9401a7d781", new Guid("03781c0c-a4a0-4bc9-866d-260b5eba8ba9") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "usm",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "66a150606380436da646dcb65a1cab93");
        }
    }
}
