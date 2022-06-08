using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddMerchantLogoToTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MerchantLogo",
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
                values: new object[] { new DateTime(2021, 9, 27, 17, 33, 18, 919, DateTimeKind.Utc).AddTicks(6254), new DateTime(2021, 9, 27, 17, 33, 18, 919, DateTimeKind.Utc).AddTicks(6269) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MerchantLogo",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 27, 14, 46, 55, 460, DateTimeKind.Utc).AddTicks(3055), new DateTime(2021, 9, 27, 14, 46, 55, 460, DateTimeKind.Utc).AddTicks(3069) });
        }
    }
}
