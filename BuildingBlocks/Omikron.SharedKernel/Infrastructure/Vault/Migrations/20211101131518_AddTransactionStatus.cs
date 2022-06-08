using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddTransactionStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionStatus",
                schema: "vault",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 11, 1, 13, 15, 18, 5, DateTimeKind.Utc).AddTicks(3907), new DateTime(2021, 11, 1, 13, 15, 18, 5, DateTimeKind.Utc).AddTicks(3918) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionStatus",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 11, 1, 9, 29, 4, 487, DateTimeKind.Utc).AddTicks(7696), new DateTime(2021, 11, 1, 9, 29, 4, 487, DateTimeKind.Utc).AddTicks(7710) });
        }
    }
}
