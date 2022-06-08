using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AccountBalanceEntryDateIndexRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountBalances_EntryDate",
                schema: "vault",
                table: "AccountBalances");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 27, 14, 46, 55, 460, DateTimeKind.Utc).AddTicks(3055), new DateTime(2021, 9, 27, 14, 46, 55, 460, DateTimeKind.Utc).AddTicks(3069) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 27, 13, 33, 23, 397, DateTimeKind.Utc).AddTicks(2247), new DateTime(2021, 9, 27, 13, 33, 23, 397, DateTimeKind.Utc).AddTicks(2259) });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalances_EntryDate",
                schema: "vault",
                table: "AccountBalances",
                column: "EntryDate",
                unique: true);
        }
    }
}
