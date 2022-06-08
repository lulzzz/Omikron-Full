using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class RelationsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Accounts_AccountId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Vehicles_VehicleId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItemValues_Accounts_AccountId",
                schema: "vault",
                table: "VaultItemValues",
                column: "AccountId",
                principalSchema: "vault",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItemValues_Vehicles_VehicleId",
                schema: "vault",
                table: "VaultItemValues",
                column: "VehicleId",
                principalSchema: "vault",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Accounts_AccountId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Vehicles_VehicleId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "vault",
                table: "VaultItemValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItemValues_Accounts_AccountId",
                schema: "vault",
                table: "VaultItemValues",
                column: "AccountId",
                principalSchema: "vault",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItemValues_Vehicles_VehicleId",
                schema: "vault",
                table: "VaultItemValues",
                column: "VehicleId",
                principalSchema: "vault",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
