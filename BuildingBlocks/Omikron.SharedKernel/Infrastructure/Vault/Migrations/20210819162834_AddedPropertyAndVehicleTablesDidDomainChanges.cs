using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddedPropertyAndVehicleTablesDidDomainChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBalances",
                schema: "vault");

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("59a9612e-3c12-46f5-863e-982444f64129"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("5d031d63-ab9a-4c01-b407-8770b2418825"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("7cf66e79-115b-4775-9e24-0b90a40e510c"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("9e4cb31c-60da-4e56-9efe-134a014114c6"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c641f883-c946-4735-9a80-e09bde294378"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                schema: "vault",
                table: "Accounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                schema: "vault",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "vault",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoanTypeId",
                schema: "vault",
                table: "Accounts",
                type: "int",
                nullable: true);

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
                name: "Properties",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MortgageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Accounts_MortgageId",
                        column: x => x.MortgageId,
                        principalSchema: "vault",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Vehicles",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAgreementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Registration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mileage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Accounts_FinancialAgreementId",
                        column: x => x.FinancialAgreementId,
                        principalSchema: "vault",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaultItems",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerIdId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ItemTypeId = table.Column<int>(type: "int", nullable: true),
                    HostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountIdentificationNumberId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaultItems_AccountIdentificationNumber_AccountIdentificationNumberId",
                        column: x => x.AccountIdentificationNumberId,
                        principalSchema: "vault",
                        principalTable: "AccountIdentificationNumber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VaultItems_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalSchema: "vault",
                        principalTable: "AccountType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VaultItems_CustomerId_OwnerIdId",
                        column: x => x.OwnerIdId,
                        principalSchema: "vault",
                        principalTable: "CustomerId",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VaultItems_VaultItemType_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalSchema: "vault",
                        principalTable: "VaultItemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VaultItemValues",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VaultItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AccountVaultItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PropertyVaultItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicleVaultItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaultItemValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaultItemValues_Accounts_AccountVaultItemId",
                        column: x => x.AccountVaultItemId,
                        principalSchema: "vault",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VaultItemValues_Properties_PropertyVaultItemId",
                        column: x => x.PropertyVaultItemId,
                        principalSchema: "vault",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VaultItemValues_Vehicles_VehicleVaultItemId",
                        column: x => x.VehicleVaultItemId,
                        principalSchema: "vault",
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LoanTypeId",
                schema: "vault",
                table: "Accounts",
                column: "LoanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_MortgageId",
                schema: "vault",
                table: "Properties",
                column: "MortgageId");

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
                name: "IX_VaultItemValues_AccountVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                column: "AccountVaultItemId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultItemValues_PropertyVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                column: "PropertyVaultItemId");

            migrationBuilder.CreateIndex(
                name: "IX_VaultItemValues_VehicleVaultItemId",
                schema: "vault",
                table: "VaultItemValues",
                column: "VehicleVaultItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_FinancialAgreementId",
                schema: "vault",
                table: "Vehicles",
                column: "FinancialAgreementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_LoanType_LoanTypeId",
                schema: "vault",
                table: "Accounts",
                column: "LoanTypeId",
                principalSchema: "vault",
                principalTable: "LoanType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_LoanType_LoanTypeId",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "LoanType",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "VaultItems",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "VaultItemValues",
                schema: "vault");

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
                name: "VaultItemType",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "Vehicles",
                schema: "vault");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_LoanTypeId",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LoanTypeId",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.CreateTable(
                name: "AccountBalances",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBalances_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "vault",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Currency", "ExternalId", "ModifiedAt", "Name", "OwnerId", "Provider", "Source", "Type" },
                values: new object[,]
                {
                    { new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Holiday", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 2 },
                    { new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Car", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 2 },
                    { new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "House", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 2 },
                    { new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Everyday", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 1 },
                    { new Guid("59a9612e-3c12-46f5-863e-982444f64129"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("59a9612e-3c12-46f5-863e-982444f64129"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Everyday", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 3 },
                    { new Guid("9e4cb31c-60da-4e56-9efe-134a014114c6"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("9e4cb31c-60da-4e56-9efe-134a014114c6"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Everyday", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 4 },
                    { new Guid("7cf66e79-115b-4775-9e24-0b90a40e510c"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("7cf66e79-115b-4775-9e24-0b90a40e510c"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Everyday", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 5 },
                    { new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Everyday", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 6 },
                    { new Guid("5d031d63-ab9a-4c01-b407-8770b2418825"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("5d031d63-ab9a-4c01-b407-8770b2418825"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Everyday", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 7 },
                    { new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Everyday", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 8 },
                    { new Guid("c641f883-c946-4735-9a80-e09bde294378"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("c641f883-c946-4735-9a80-e09bde294378"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Everyday", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Santader", 1, 9 }
                });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "AccountBalances",
                columns: new[] { "Id", "AccountId", "Amount", "CreatedAt", "EntryDate", "ModifiedAt", "Type" },
                values: new object[,]
                {
                    { new Guid("2dd8db17-8bcf-4edb-b94e-a06f901d0558"), new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), 1321.23m, new DateTime(2021, 8, 6, 10, 28, 37, 972, DateTimeKind.Utc).AddTicks(8371), new DateTime(2021, 8, 6, 10, 28, 37, 972, DateTimeKind.Utc).AddTicks(9977), new DateTime(2021, 8, 6, 10, 28, 37, 972, DateTimeKind.Utc).AddTicks(8389), 2 },
                    { new Guid("0e9732a0-d596-4e93-8dcd-22ed9f685bba"), new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), 13223.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2033), new DateTime(2021, 8, 4, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2054), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2036), 2 },
                    { new Guid("b2f2c6d8-c14c-4156-aec4-ecde51c1d74a"), new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"), 2223.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2088), new DateTime(2021, 8, 4, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2094), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2089), 2 },
                    { new Guid("4d04f7a8-8e03-4563-abe5-16503b5e8051"), new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"), 5223.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2096), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2100), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2097), 2 },
                    { new Guid("92271498-4bb6-4079-975b-7df225e7575b"), new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"), 5223.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2153), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2158), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2154), 2 },
                    { new Guid("2e97f1d5-fa57-41fe-84a1-e8348b69c75e"), new Guid("59a9612e-3c12-46f5-863e-982444f64129"), 2265.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2160), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2164), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2160), 2 },
                    { new Guid("8364a3f7-3aa0-4122-af43-1632e551bf6d"), new Guid("9e4cb31c-60da-4e56-9efe-134a014114c6"), 52625.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2165), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2169), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2166), 2 },
                    { new Guid("ab54d989-1ef7-4244-88c4-dbcc7fb810c7"), new Guid("7cf66e79-115b-4775-9e24-0b90a40e510c"), 232625.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2170), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2174), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2171), 2 },
                    { new Guid("c329ec9e-2663-4dad-8bc5-d898563b745d"), new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"), -1325.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2176), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2185), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2176), 2 },
                    { new Guid("d0febb10-b10d-456e-a959-d9af8ad70d13"), new Guid("5d031d63-ab9a-4c01-b407-8770b2418825"), -11325.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2186), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2190), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2187), 2 },
                    { new Guid("e94db63c-f3cf-4ed9-ac55-2fa77ecc59db"), new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"), -66325.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2191), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2195), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2192), 2 },
                    { new Guid("2d7fef9c-b4d7-416d-81b7-95488878f86c"), new Guid("c641f883-c946-4735-9a80-e09bde294378"), -26325.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2196), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2200), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2197), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalances_AccountId",
                schema: "vault",
                table: "AccountBalances",
                column: "AccountId");
        }
    }
}
