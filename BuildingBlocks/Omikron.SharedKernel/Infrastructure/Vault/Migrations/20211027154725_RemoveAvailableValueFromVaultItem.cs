using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class RemoveAvailableValueFromVaultItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableValue",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 27, 15, 47, 25, 101, DateTimeKind.Utc).AddTicks(1135), new DateTime(2021, 10, 27, 15, 47, 25, 101, DateTimeKind.Utc).AddTicks(1145) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AvailableValue",
                schema: "vault",
                table: "VaultItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 26, 15, 5, 28, 96, DateTimeKind.Utc).AddTicks(5406), new DateTime(2021, 10, 26, 15, 5, 28, 96, DateTimeKind.Utc).AddTicks(5415) });
        }
    }
}
