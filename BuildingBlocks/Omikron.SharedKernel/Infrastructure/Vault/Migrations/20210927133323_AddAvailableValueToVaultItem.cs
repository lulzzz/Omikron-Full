using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddAvailableValueToVaultItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountSource",
                schema: "vault",
                table: "VaultItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "AvailableValue",
                schema: "vault",
                table: "VaultItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Tenant",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CssFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AzureAssetStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "Tenant",
                columns: new[] { "Id", "AzureAssetStatus", "CreatedAt", "CssFile", "HostName", "Identifier", "Logo", "ModifiedAt", "Name", "Status", "Type" },
                values: new object[] { "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8", 4, new DateTime(2021, 9, 27, 13, 33, 23, 397, DateTimeKind.Utc).AddTicks(2247), null, "Omikron", "Omikron", "images/applicita-software-logo.png", new DateTime(2021, 9, 27, 13, 33, 23, 397, DateTimeKind.Utc).AddTicks(2259), "Omikron Money Solution", 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalances_EntryDate",
                schema: "vault",
                table: "AccountBalances",
                column: "EntryDate",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenant",
                schema: "vault");

            migrationBuilder.DropIndex(
                name: "IX_AccountBalances_EntryDate",
                schema: "vault",
                table: "AccountBalances");

            migrationBuilder.DropColumn(
                name: "AccountSource",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropColumn(
                name: "AvailableValue",
                schema: "vault",
                table: "VaultItems");
        }
    }
}
