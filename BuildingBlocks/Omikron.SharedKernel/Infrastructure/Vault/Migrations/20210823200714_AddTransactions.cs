using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "vault",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Currency", "ExpiryDate", "ExternalId", "IdentificationNumber", "ImageUrl", "LoanType", "ModifiedAt", "Name", "OwnerId", "Provider", "Source", "Type" },
                values: new object[] { new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), new DateTime(2021, 8, 23, 20, 7, 13, 905, DateTimeKind.Utc).AddTicks(3485), "GBP", new DateTime(2021, 8, 23, 20, 7, 43, 908, DateTimeKind.Utc).AddTicks(8445), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), "111111•11111111", "", null, new DateTime(2021, 8, 23, 20, 7, 13, 905, DateTimeKind.Utc).AddTicks(3500), "Test", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Lloyds", 1, 1 });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "Amount", "Category", "CreatedAt", "Currency", "Date", "MerchantIcon", "MerchantName", "ModifiedAt" },
                values: new object[,]
                {
                    { new Guid("93024c9c-013b-4635-82ba-842f4067f8f9"), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), 13.23m, "Electronics", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(1984), "GBP", new DateTime(2021, 8, 21, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(4559), null, "Amazon", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(1997) },
                    { new Guid("6a18dcff-d1f0-4067-bcd9-fc13c863e3c3"), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), 16.42m, "Gaming", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(4985), "GBP", new DateTime(2021, 8, 21, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(5005), null, "Steam", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(4988) },
                    { new Guid("ea759c8f-6ee4-4231-83c0-27211c50f1ca"), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), 76.32m, "Groceries", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(5008), "GBP", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(5014), null, "Target", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(5009) },
                    { new Guid("a7a9dfc3-67c2-401c-b436-74888bd9bf54"), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), 123.23m, "Groceries", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(5015), "GBP", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(5020), null, "Lidl", new DateTime(2021, 8, 23, 20, 7, 13, 912, DateTimeKind.Utc).AddTicks(5016) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                schema: "vault",
                table: "Transactions",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "vault");

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("48ab7b04-218d-4205-a177-437db3d0a552"));
        }
    }
}
