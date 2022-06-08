using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class MerchantsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 1, 1, 38, 56, 921, DateTimeKind.Utc).AddTicks(1534), new DateTime(2021, 10, 1, 1, 38, 56, 921, DateTimeKind.Utc).AddTicks(1544) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 1, 0, 34, 6, 308, DateTimeKind.Utc).AddTicks(8916), new DateTime(2021, 10, 1, 0, 34, 6, 308, DateTimeKind.Utc).AddTicks(8928) });
        }
    }
}
