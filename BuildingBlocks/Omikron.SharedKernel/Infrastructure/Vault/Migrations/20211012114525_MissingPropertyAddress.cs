using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class MissingPropertyAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 12, 11, 45, 25, 508, DateTimeKind.Utc).AddTicks(5520), new DateTime(2021, 10, 12, 11, 45, 25, 508, DateTimeKind.Utc).AddTicks(5525) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 7, 19, 44, 54, 228, DateTimeKind.Utc).AddTicks(2120), new DateTime(2021, 10, 7, 19, 44, 54, 228, DateTimeKind.Utc).AddTicks(2128) });
        }
    }
}
