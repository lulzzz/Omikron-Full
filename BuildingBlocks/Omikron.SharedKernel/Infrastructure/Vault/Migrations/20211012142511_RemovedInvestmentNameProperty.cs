using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class RemovedInvestmentNameProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvestmentName",
                schema: "vault",
                table: "Investments");

            migrationBuilder.Sql("UPDATE [vault].[Investments] SET Name = 'N/A'");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "vault",
                table: "Investments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 12, 14, 25, 11, 259, DateTimeKind.Utc).AddTicks(150), new DateTime(2021, 10, 12, 14, 25, 11, 259, DateTimeKind.Utc).AddTicks(161) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "vault",
                table: "Investments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "InvestmentName",
                schema: "vault",
                table: "Investments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 12, 12, 59, 49, 441, DateTimeKind.Utc).AddTicks(3138), new DateTime(2021, 10, 12, 12, 59, 49, 441, DateTimeKind.Utc).AddTicks(3151) });
        }
    }
}
