using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class MoreAccountsAsSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("2be0a47b-63ca-4831-8f88-c78b4a041f3a"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("2f778481-e5e4-4199-bef7-7eff404090e2"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("d2cfab5b-dabd-47f7-a1b4-cf4d92d51eb2"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("eeab52ea-f443-4e28-8a6f-65de9ee231a2"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("ef657a9c-2862-46e2-8de0-777234b8eed4"));

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
                    { new Guid("92271498-4bb6-4079-975b-7df225e7575b"), new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"), 5223.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2153), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2158), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2154), 2 }
                });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Currency", "ExternalId", "ModifiedAt", "Name", "OwnerId", "Provider", "Source", "Type" },
                values: new object[,]
                {
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
                    { new Guid("2e97f1d5-fa57-41fe-84a1-e8348b69c75e"), new Guid("59a9612e-3c12-46f5-863e-982444f64129"), 2265.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2160), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2164), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2160), 2 },
                    { new Guid("8364a3f7-3aa0-4122-af43-1632e551bf6d"), new Guid("9e4cb31c-60da-4e56-9efe-134a014114c6"), 52625.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2165), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2169), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2166), 2 },
                    { new Guid("ab54d989-1ef7-4244-88c4-dbcc7fb810c7"), new Guid("7cf66e79-115b-4775-9e24-0b90a40e510c"), 232625.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2170), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2174), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2171), 2 },
                    { new Guid("c329ec9e-2663-4dad-8bc5-d898563b745d"), new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"), -1325.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2176), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2185), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2176), 2 },
                    { new Guid("d0febb10-b10d-456e-a959-d9af8ad70d13"), new Guid("5d031d63-ab9a-4c01-b407-8770b2418825"), -11325.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2186), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2190), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2187), 2 },
                    { new Guid("e94db63c-f3cf-4ed9-ac55-2fa77ecc59db"), new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"), -66325.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2191), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2195), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2192), 2 },
                    { new Guid("2d7fef9c-b4d7-416d-81b7-95488878f86c"), new Guid("c641f883-c946-4735-9a80-e09bde294378"), -26325.23m, new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2196), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2200), new DateTime(2021, 8, 6, 10, 28, 37, 973, DateTimeKind.Utc).AddTicks(2197), 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("0e9732a0-d596-4e93-8dcd-22ed9f685bba"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("2d7fef9c-b4d7-416d-81b7-95488878f86c"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("2dd8db17-8bcf-4edb-b94e-a06f901d0558"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("2e97f1d5-fa57-41fe-84a1-e8348b69c75e"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("4d04f7a8-8e03-4563-abe5-16503b5e8051"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("8364a3f7-3aa0-4122-af43-1632e551bf6d"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("92271498-4bb6-4079-975b-7df225e7575b"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("ab54d989-1ef7-4244-88c4-dbcc7fb810c7"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("b2f2c6d8-c14c-4156-aec4-ecde51c1d74a"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("c329ec9e-2663-4dad-8bc5-d898563b745d"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("d0febb10-b10d-456e-a959-d9af8ad70d13"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("e94db63c-f3cf-4ed9-ac55-2fa77ecc59db"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"));

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
                keyValue: new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"));

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

            migrationBuilder.InsertData(
                schema: "vault",
                table: "AccountBalances",
                columns: new[] { "Id", "AccountId", "Amount", "CreatedAt", "EntryDate", "ModifiedAt", "Type" },
                values: new object[,]
                {
                    { new Guid("2f778481-e5e4-4199-bef7-7eff404090e2"), new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), 1321.23m, new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(2572), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(4052), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(2587), 2 },
                    { new Guid("eeab52ea-f443-4e28-8a6f-65de9ee231a2"), new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), 13223.23m, new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6285), new DateTime(2021, 8, 2, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6301), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6288), 2 },
                    { new Guid("2be0a47b-63ca-4831-8f88-c78b4a041f3a"), new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"), 2223.23m, new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6332), new DateTime(2021, 8, 2, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6338), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6333), 2 },
                    { new Guid("d2cfab5b-dabd-47f7-a1b4-cf4d92d51eb2"), new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"), 5223.23m, new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6340), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6344), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6340), 2 },
                    { new Guid("ef657a9c-2862-46e2-8de0-777234b8eed4"), new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"), 5223.23m, new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6345), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6351), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6346), 2 }
                });
        }
    }
}
