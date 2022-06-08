using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddCreditDebitIndicator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditDebitIndicator",
                schema: "vault",
                table: "VaultItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditDebitIndicator",
                schema: "vault",
                table: "AccountBalances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 27, 18, 49, 32, 453, DateTimeKind.Utc).AddTicks(7853), new DateTime(2021, 10, 27, 18, 49, 32, 453, DateTimeKind.Utc).AddTicks(7863) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditDebitIndicator",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropColumn(
                name: "CreditDebitIndicator",
                schema: "vault",
                table: "AccountBalances");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 27, 15, 47, 25, 101, DateTimeKind.Utc).AddTicks(1135), new DateTime(2021, 10, 27, 15, 47, 25, 101, DateTimeKind.Utc).AddTicks(1145) });
        }
    }
}
