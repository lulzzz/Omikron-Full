using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddTransactionsMockDataForDeveloping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "vault",
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Currency", "ExpiryDate", "ExternalId", "IdentificationNumber", "ImageUrl", "LoanType", "ModifiedAt", "Name", "OwnerId", "Provider", "Source", "Type" },
                values: new object[] { new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), new DateTime(2021, 8, 24, 12, 16, 46, 267, DateTimeKind.Utc).AddTicks(5370), "GBP", new DateTime(2021, 8, 24, 12, 17, 16, 270, DateTimeKind.Utc).AddTicks(2641), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), "111111•11111111", "", null, new DateTime(2021, 8, 24, 12, 16, 46, 267, DateTimeKind.Utc).AddTicks(5383), "Test", "D15D234D-D886-4F14-B3B0-480CBBABF6B1", "Lloyds", 1, 1 });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "Amount", "Category", "CreatedAt", "Currency", "Date", "MerchantIcon", "MerchantName", "ModifiedAt" },
                values: new object[,]
                {
                    { new Guid("93024c9c-013b-4635-82ba-842f4067f8f9"), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), 13.23m, "Electronics", new DateTime(2021, 8, 24, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(8156), "GBP", new DateTime(2021, 8, 22, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(7766), null, "Amazon", new DateTime(2021, 8, 24, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(8159) },
                    { new Guid("6a18dcff-d1f0-4067-bcd9-fc13c863e3c3"), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), 16.42m, "Gaming", new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(75), "GBP", new DateTime(2021, 8, 22, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(7766), null, "Steam", new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(78) },
                    { new Guid("ea759c8f-6ee4-4231-83c0-27211c50f1ca"), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), 76.32m, "Groceries", new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(93), "GBP", new DateTime(2021, 8, 24, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(7766), null, "Target", new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(94) },
                    { new Guid("a7a9dfc3-67c2-401c-b436-74888bd9bf54"), new Guid("48ab7b04-218d-4205-a177-437db3d0a552"), 123.23m, "Groceries", new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(99), "GBP", new DateTime(2021, 8, 24, 12, 16, 46, 273, DateTimeKind.Utc).AddTicks(7766), null, "Lidl", new DateTime(2021, 8, 24, 12, 16, 46, 274, DateTimeKind.Utc).AddTicks(100) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("6a18dcff-d1f0-4067-bcd9-fc13c863e3c3"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("93024c9c-013b-4635-82ba-842f4067f8f9"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("a7a9dfc3-67c2-401c-b436-74888bd9bf54"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("ea759c8f-6ee4-4231-83c0-27211c50f1ca"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("48ab7b04-218d-4205-a177-437db3d0a552"));
        }
    }
}
