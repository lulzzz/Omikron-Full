using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddTransDetailsAndCreditDebitIndicator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditDebitIndicator",
                schema: "vault",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionInformation",
                schema: "vault",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 11, 1, 9, 29, 4, 487, DateTimeKind.Utc).AddTicks(7696), new DateTime(2021, 11, 1, 9, 29, 4, 487, DateTimeKind.Utc).AddTicks(7710) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditDebitIndicator",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionInformation",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 27, 18, 49, 32, 453, DateTimeKind.Utc).AddTicks(7853), new DateTime(2021, 10, 27, 18, 49, 32, 453, DateTimeKind.Utc).AddTicks(7863) });
        }
    }
}
