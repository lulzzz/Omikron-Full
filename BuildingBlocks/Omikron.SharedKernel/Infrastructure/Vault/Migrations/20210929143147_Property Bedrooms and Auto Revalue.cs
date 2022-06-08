using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class PropertyBedroomsandAutoRevalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutomaticallyReValueProperty",
                schema: "vault",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBedRooms",
                schema: "vault",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 29, 14, 31, 46, 679, DateTimeKind.Utc).AddTicks(1620), new DateTime(2021, 9, 29, 14, 31, 46, 679, DateTimeKind.Utc).AddTicks(1626) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutomaticallyReValueProperty",
                schema: "vault",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "NumberOfBedRooms",
                schema: "vault",
                table: "Properties");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 28, 7, 25, 57, 694, DateTimeKind.Utc).AddTicks(6531), new DateTime(2021, 9, 28, 7, 25, 57, 694, DateTimeKind.Utc).AddTicks(6548) });
        }
    }
}
