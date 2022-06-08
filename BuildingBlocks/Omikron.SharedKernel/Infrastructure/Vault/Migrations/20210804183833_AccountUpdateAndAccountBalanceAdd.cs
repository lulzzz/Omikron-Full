using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AccountUpdateAndAccountBalanceAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountSource",
                schema: "vault",
                table: "Accounts",
                newName: "Source");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                schema: "vault",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                schema: "vault",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "vault",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                schema: "vault",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "vault",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BalanceHistory",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceHistory_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "vault",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"),
                columns: new[] { "Currency", "ExternalId", "Name", "Provider", "Type" },
                values: new object[] { "GBP", new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), "Holiday", "Santader", 2 });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Currency", "ExternalId", "ModifiedAt", "Name", "OwnerId", "Provider", "Source", "Type" },
                values: new object[,]
                {
                    { new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Car", "158A357A-0495-4C43-850E-20AFB1F365E3", "Santader", 1, 2 },
                    { new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "House", "158A357A-0495-4C43-850E-20AFB1F365E3", "Santader", 1, 2 },
                    { new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "GBP", new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "Everyday", "158A357A-0495-4C43-850E-20AFB1F365E3", "Santader", 1, 1 }
                });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "BalanceHistory",
                columns: new[] { "Id", "AccountId", "Amount", "CreatedAt", "EntryDate", "ModifiedAt", "Type" },
                values: new object[,]
                {
                    { new Guid("2f778481-e5e4-4199-bef7-7eff404090e2"), new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), 1321.23m, new DateTime(2021, 8, 4, 18, 38, 32, 894, DateTimeKind.Utc).AddTicks(7054), new DateTime(2021, 8, 4, 18, 38, 32, 894, DateTimeKind.Utc).AddTicks(8704), new DateTime(2021, 8, 4, 18, 38, 32, 894, DateTimeKind.Utc).AddTicks(7070), 2 },
                    { new Guid("eeab52ea-f443-4e28-8a6f-65de9ee231a2"), new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), 13223.23m, new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(727), new DateTime(2021, 8, 2, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(743), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(730), 2 }
                });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "BalanceHistory",
                columns: new[] { "Id", "AccountId", "Amount", "CreatedAt", "EntryDate", "ModifiedAt", "Type" },
                values: new object[] { new Guid("2be0a47b-63ca-4831-8f88-c78b4a041f3a"), new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"), 2223.23m, new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(774), new DateTime(2021, 8, 2, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(780), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(775), 2 });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "BalanceHistory",
                columns: new[] { "Id", "AccountId", "Amount", "CreatedAt", "EntryDate", "ModifiedAt", "Type" },
                values: new object[] { new Guid("d2cfab5b-dabd-47f7-a1b4-cf4d92d51eb2"), new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"), 5223.23m, new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(781), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(786), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(782), 2 });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "BalanceHistory",
                columns: new[] { "Id", "AccountId", "Amount", "CreatedAt", "EntryDate", "ModifiedAt", "Type" },
                values: new object[] { new Guid("ef657a9c-2862-46e2-8de0-777234b8eed4"), new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"), 5223.23m, new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(787), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(791), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(788), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceHistory_AccountId",
                schema: "vault",
                table: "BalanceHistory",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceHistory",
                schema: "vault");

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"));

            migrationBuilder.DropColumn(
                name: "Currency",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Provider",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "vault",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Source",
                schema: "vault",
                table: "Accounts",
                newName: "AccountSource");
        }
    }
}
