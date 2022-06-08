using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class MerchantsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MerchantLogo",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "MerchantName",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "MerchantId",
                schema: "vault",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Merchants",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 29, 16, 33, 35, 223, DateTimeKind.Utc).AddTicks(1127), new DateTime(2021, 9, 29, 16, 33, 35, 223, DateTimeKind.Utc).AddTicks(1137) });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MerchantId",
                schema: "vault",
                table: "Transactions",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_Name",
                schema: "vault",
                table: "Merchants",
                column: "Name",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Merchants_MerchantId",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Merchants",
                schema: "vault");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_MerchantId",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "MerchantLogo",
                schema: "vault",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantName",
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
                values: new object[] { new DateTime(2021, 9, 28, 7, 25, 57, 694, DateTimeKind.Utc).AddTicks(6531), new DateTime(2021, 9, 28, 7, 25, 57, 694, DateTimeKind.Utc).AddTicks(6548) });
        }
    }
}
