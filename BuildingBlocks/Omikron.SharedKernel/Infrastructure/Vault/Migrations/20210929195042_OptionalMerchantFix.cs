using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class OptionalMerchantFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 29, 19, 50, 42, 529, DateTimeKind.Utc).AddTicks(8894), new DateTime(2021, 9, 29, 19, 50, 42, 529, DateTimeKind.Utc).AddTicks(8904) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 29, 19, 1, 57, 760, DateTimeKind.Utc).AddTicks(8818), new DateTime(2021, 9, 29, 19, 1, 57, 760, DateTimeKind.Utc).AddTicks(8834) });
        }
    }
}
