using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class RelationshipsChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_LoanType_LoanTypeId",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Accounts_MortgageId",
                schema: "vault",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItems_AccountIdentificationNumber_AccountIdentificationNumberId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItems_AccountType_AccountTypeId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItems_CustomerId_OwnerIdId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItems_VaultItemType_ItemTypeId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Accounts_AccountVaultItemId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Properties_PropertyVaultItemId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Vehicles_VehicleVaultItemId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Accounts_FinancialAgreementId",
                schema: "vault",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "AccountIdentificationNumber",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "AccountType",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "CustomerId",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "LoanType",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "VaultItemType",
                schema: "vault");

            migrationBuilder.DropIndex(
                name: "IX_VaultItems_AccountIdentificationNumberId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropIndex(
                name: "IX_VaultItems_AccountTypeId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropIndex(
                name: "IX_VaultItems_ItemTypeId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropIndex(
                name: "IX_VaultItems_OwnerIdId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_LoanTypeId",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "VaultItemId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropColumn(
                name: "AccountIdentificationNumberId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropColumn(
                name: "OwnerIdId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.RenameColumn(
                name: "VehicleVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "VehicleId");

            migrationBuilder.RenameColumn(
                name: "PropertyVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "PropertyId");

            migrationBuilder.RenameColumn(
                name: "AccountVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_VaultItemValues_VehicleVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "IX_VaultItemValues_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_VaultItemValues_PropertyVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "IX_VaultItemValues_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VaultItemValues_AccountVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "IX_VaultItemValues_AccountId");

            migrationBuilder.RenameColumn(
                name: "ItemTypeId",
                schema: "vault",
                table: "VaultItems",
                newName: "AccountType");

            migrationBuilder.RenameColumn(
                name: "LoanTypeId",
                schema: "vault",
                table: "Accounts",
                newName: "LoanType");

            migrationBuilder.AlterColumn<Guid>(
                name: "FinancialAgreementId",
                schema: "vault",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "vault",
                table: "VaultItemValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountIdentificationNumber",
                schema: "vault",
                table: "VaultItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                schema: "vault",
                table: "VaultItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                schema: "vault",
                table: "VaultItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "MortgageId",
                schema: "vault",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Accounts_MortgageId",
                schema: "vault",
                table: "Properties",
                column: "MortgageId",
                principalSchema: "vault",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_VaultItemValues_Properties_PropertyId",
                schema: "vault",
                table: "VaultItemValues",
                column: "PropertyId",
                principalSchema: "vault",
                principalTable: "Properties",
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Accounts_FinancialAgreementId",
                schema: "vault",
                table: "Vehicles",
                column: "FinancialAgreementId",
                principalSchema: "vault",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Accounts_MortgageId",
                schema: "vault",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Accounts_AccountId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Properties_PropertyId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VaultItemValues_Vehicles_VehicleId",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Accounts_FinancialAgreementId",
                schema: "vault",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "vault",
                table: "VaultItemValues");

            migrationBuilder.DropColumn(
                name: "AccountIdentificationNumber",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropColumn(
                name: "ItemType",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "vault",
                table: "VaultItems");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "VehicleVaultItemId");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "PropertyVaultItemId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "AccountVaultItemId");

            migrationBuilder.RenameIndex(
                name: "IX_VaultItemValues_VehicleId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "IX_VaultItemValues_VehicleVaultItemId");

            migrationBuilder.RenameIndex(
                name: "IX_VaultItemValues_PropertyId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "IX_VaultItemValues_PropertyVaultItemId");

            migrationBuilder.RenameIndex(
                name: "IX_VaultItemValues_AccountId",
                schema: "vault",
                table: "VaultItemValues",
                newName: "IX_VaultItemValues_AccountVaultItemId");

            migrationBuilder.RenameColumn(
                name: "AccountType",
                schema: "vault",
                table: "VaultItems",
                newName: "ItemTypeId");

            migrationBuilder.RenameColumn(
                name: "LoanType",
                schema: "vault",
                table: "Accounts",
                newName: "LoanTypeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "FinancialAgreementId",
                schema: "vault",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AccountIdentificationNumberId",
                schema: "vault",
                table: "VaultItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountTypeId",
                schema: "vault",
                table: "VaultItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerIdId",
                schema: "vault",
                table: "VaultItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MortgageId",
                schema: "vault",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AccountIdentificationNumber",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SortCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountIdentificationNumber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountType",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerId",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanType",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaultItemType",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultItemType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaultItems_AccountIdentificationNumberId",
                schema: "vault",
                table: "VaultItems",
                column: "AccountIdentificationNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultItems_AccountTypeId",
                schema: "vault",
                table: "VaultItems",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultItems_ItemTypeId",
                schema: "vault",
                table: "VaultItems",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultItems_OwnerIdId",
                schema: "vault",
                table: "VaultItems",
                column: "OwnerIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LoanTypeId",
                schema: "vault",
                table: "Accounts",
                column: "LoanTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_LoanType_LoanTypeId",
                schema: "vault",
                table: "Accounts",
                column: "LoanTypeId",
                principalSchema: "vault",
                principalTable: "LoanType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Accounts_MortgageId",
                schema: "vault",
                table: "Properties",
                column: "MortgageId",
                principalSchema: "vault",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItems_AccountIdentificationNumber_AccountIdentificationNumberId",
                schema: "vault",
                table: "VaultItems",
                column: "AccountIdentificationNumberId",
                principalSchema: "vault",
                principalTable: "AccountIdentificationNumber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItems_AccountType_AccountTypeId",
                schema: "vault",
                table: "VaultItems",
                column: "AccountTypeId",
                principalSchema: "vault",
                principalTable: "AccountType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItems_CustomerId_OwnerIdId",
                schema: "vault",
                table: "VaultItems",
                column: "OwnerIdId",
                principalSchema: "vault",
                principalTable: "CustomerId",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItems_VaultItemType_ItemTypeId",
                schema: "vault",
                table: "VaultItems",
                column: "ItemTypeId",
                principalSchema: "vault",
                principalTable: "VaultItemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItemValues_Accounts_AccountVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                column: "AccountVaultItemId",
                principalSchema: "vault",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItemValues_Properties_PropertyVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                column: "PropertyVaultItemId",
                principalSchema: "vault",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VaultItemValues_Vehicles_VehicleVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                column: "VehicleVaultItemId",
                principalSchema: "vault",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Accounts_FinancialAgreementId",
                schema: "vault",
                table: "Vehicles",
                column: "FinancialAgreementId",
                principalSchema: "vault",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
