using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class RefreshHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "RefreshHistories",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshHistories", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "AccountBalances",
                columns: new[] { "Id", "AccountId", "Amount", "CreatedAt", "EntryDate", "ModifiedAt", "Type" },
                values: new object[,]
                {
                    { new Guid("36916e63-7fd2-40e6-ac49-89d7d7b49feb"), new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), 1321.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(3179), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(4829), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(3197), 2 },
                    { new Guid("e6396e72-a5d8-456b-9aad-2ed8a07e23c1"), new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), 13223.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6876), new DateTime(2021, 8, 13, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6902), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6879), 2 },
                    { new Guid("8fb12fd7-6094-4861-b08c-ccbb3eacfcc6"), new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"), 2223.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6937), new DateTime(2021, 8, 13, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6943), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6938), 2 },
                    { new Guid("4b1f5b7b-c84c-458b-a92b-bee55f9ff2e2"), new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"), 5223.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6945), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6949), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6946), 2 },
                    { new Guid("0359d5fd-4c73-4341-91a9-4c25a6680f8c"), new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"), 5223.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6950), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6954), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6951), 2 },
                    { new Guid("5ae0f170-616b-4880-bf12-81ec0e72a8ac"), new Guid("59a9612e-3c12-46f5-863e-982444f64129"), 2265.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6955), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6960), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6956), 2 },
                    { new Guid("2eaac991-16fd-4da9-9501-401ad300a5c1"), new Guid("9e4cb31c-60da-4e56-9efe-134a014114c6"), 52625.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6961), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6965), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6962), 2 },
                    { new Guid("4d4dad1b-a197-4108-8a85-ec9316dc00d3"), new Guid("7cf66e79-115b-4775-9e24-0b90a40e510c"), 232625.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6966), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6970), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6967), 2 },
                    { new Guid("6ace353c-f1cc-4c08-9c5d-440eb0657b85"), new Guid("6a9f3896-9901-4024-ab46-9ffe997cace3"), -1325.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6972), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6980), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6973), 2 },
                    { new Guid("e268ae36-2de7-43ca-80a8-a8a3e30f668d"), new Guid("5d031d63-ab9a-4c01-b407-8770b2418825"), -11325.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6982), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6985), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6983), 2 },
                    { new Guid("6b4082da-f789-493f-8150-6f8f51bfed3a"), new Guid("0a4c0cbd-c03b-4206-a633-46fb118177c3"), -66325.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6987), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6991), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6987), 2 },
                    { new Guid("7fec44ce-8e0d-475d-bf23-f351afaa59cc"), new Guid("c641f883-c946-4735-9a80-e09bde294378"), -26325.23m, new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6992), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6996), new DateTime(2021, 8, 15, 20, 57, 34, 53, DateTimeKind.Utc).AddTicks(6993), 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshHistories",
                schema: "vault");

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("0359d5fd-4c73-4341-91a9-4c25a6680f8c"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("2eaac991-16fd-4da9-9501-401ad300a5c1"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("36916e63-7fd2-40e6-ac49-89d7d7b49feb"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("4b1f5b7b-c84c-458b-a92b-bee55f9ff2e2"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("4d4dad1b-a197-4108-8a85-ec9316dc00d3"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("5ae0f170-616b-4880-bf12-81ec0e72a8ac"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("6ace353c-f1cc-4c08-9c5d-440eb0657b85"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("6b4082da-f789-493f-8150-6f8f51bfed3a"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("7fec44ce-8e0d-475d-bf23-f351afaa59cc"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("8fb12fd7-6094-4861-b08c-ccbb3eacfcc6"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("e268ae36-2de7-43ca-80a8-a8a3e30f668d"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("e6396e72-a5d8-456b-9aad-2ed8a07e23c1"));

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
        }
    }
}
