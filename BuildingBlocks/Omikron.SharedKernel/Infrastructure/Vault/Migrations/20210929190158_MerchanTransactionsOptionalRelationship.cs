using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class MerchanTransactionsOptionalRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Merchants_MerchantId",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 29, 19, 1, 57, 760, DateTimeKind.Utc).AddTicks(8818), new DateTime(2021, 9, 29, 19, 1, 57, 760, DateTimeKind.Utc).AddTicks(8834) });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Merchants_MerchantId",
                schema: "vault",
                table: "Transactions",
                column: "MerchantId",
                principalSchema: "vault",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Merchants_MerchantId",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 29, 16, 33, 35, 223, DateTimeKind.Utc).AddTicks(1127), new DateTime(2021, 9, 29, 16, 33, 35, 223, DateTimeKind.Utc).AddTicks(1137) });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Merchants_MerchantId",
                schema: "vault",
                table: "Transactions",
                column: "MerchantId",
                principalSchema: "vault",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
