using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class PersonalItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalItems",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAgreementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_PersonalItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalItems_Accounts_FinancialAgreementId",
                        column: x => x.FinancialAgreementId,
                        principalSchema: "vault",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalItemValues",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalItemValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalItemValues_PersonalItems_PersonalItemId",
                        column: x => x.PersonalItemId,
                        principalSchema: "vault",
                        principalTable: "PersonalItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("48ab7b04-218d-4205-a177-437db3d0a552"),
                columns: new[] { "CreatedAt", "ExpiryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 16, 27, 451, DateTimeKind.Utc).AddTicks(7538), new DateTime(2021, 9, 12, 20, 16, 57, 454, DateTimeKind.Utc).AddTicks(4798), new DateTime(2021, 9, 12, 20, 16, 27, 451, DateTimeKind.Utc).AddTicks(7546) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("6a18dcff-d1f0-4067-bcd9-fc13c863e3c3"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(1490), new DateTime(2021, 9, 10, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(46), new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(1492) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("93024c9c-013b-4635-82ba-842f4067f8f9"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(318), new DateTime(2021, 9, 10, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(46), new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(320) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("a7a9dfc3-67c2-401c-b436-74888bd9bf54"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(1505), new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(46), new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(1506) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("ea759c8f-6ee4-4231-83c0-27211c50f1ca"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(1501), new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(46), new DateTime(2021, 9, 12, 20, 16, 27, 458, DateTimeKind.Utc).AddTicks(1501) });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalItems_FinancialAgreementId",
                schema: "vault",
                table: "PersonalItems",
                column: "FinancialAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalItemValues_PersonalItemId",
                schema: "vault",
                table: "PersonalItemValues",
                column: "PersonalItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalItemValues",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "PersonalItems",
                schema: "vault");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("48ab7b04-218d-4205-a177-437db3d0a552"),
                columns: new[] { "CreatedAt", "ExpiryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 24, 12, 16, 46, 267, DateTimeKind.Utc).AddTicks(5370), new DateTime(2021, 8, 24, 12, 17, 16, 270, DateTimeKind.Utc).AddTicks(2641), new DateTime(2021, 8, 24, 12, 16, 46, 267, DateTimeKind.Utc).AddTicks(5383) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("6a18dcff-d1f0-4067-bcd9-fc13c863e3c3"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(75), new DateTime(2021, 8, 22, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(7766), new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(78) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("93024c9c-013b-4635-82ba-842f4067f8f9"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 24, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(8156), new DateTime(2021, 8, 22, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(7766), new DateTime(2021, 8, 24, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(8159) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("a7a9dfc3-67c2-401c-b436-74888bd9bf54"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(99), new DateTime(2021, 8, 24, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(7766), new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(100) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("ea759c8f-6ee4-4231-83c0-27211c50f1ca"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(93), new DateTime(2021, 8, 24, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(7766), new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(94) });
        }
    }
}
