using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class BalanceHistoryNameChangedToAccountBalances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceHistory_Accounts_AccountId",
                schema: "vault",
                table: "BalanceHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BalanceHistory",
                schema: "vault",
                table: "BalanceHistory");

            migrationBuilder.RenameTable(
                name: "BalanceHistory",
                schema: "vault",
                newName: "AccountBalances",
                newSchema: "vault");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceHistory_AccountId",
                schema: "vault",
                table: "AccountBalances",
                newName: "IX_AccountBalances_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountBalances",
                schema: "vault",
                table: "AccountBalances",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("2be0a47b-63ca-4831-8f88-c78b4a041f3a"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6332), new DateTime(2021, 8, 2, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6338), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6333) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("2f778481-e5e4-4199-bef7-7eff404090e2"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(2572), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(4052), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(2587) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("d2cfab5b-dabd-47f7-a1b4-cf4d92d51eb2"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6340), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6344), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6340) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("eeab52ea-f443-4e28-8a6f-65de9ee231a2"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6285), new DateTime(2021, 8, 2, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6301), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6288) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "AccountBalances",
                keyColumn: "Id",
                keyValue: new Guid("ef657a9c-2862-46e2-8de0-777234b8eed4"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6345), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6351), new DateTime(2021, 8, 4, 22, 59, 47, 557, DateTimeKind.Utc).AddTicks(6346) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"),
                column: "OwnerId",
                value: "D15D234D-D886-4F14-B3B0-480CBBABF6B1");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"),
                column: "OwnerId",
                value: "D15D234D-D886-4F14-B3B0-480CBBABF6B1");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"),
                column: "OwnerId",
                value: "D15D234D-D886-4F14-B3B0-480CBBABF6B1");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"),
                column: "OwnerId",
                value: "D15D234D-D886-4F14-B3B0-480CBBABF6B1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBalances_Accounts_AccountId",
                schema: "vault",
                table: "AccountBalances",
                column: "AccountId",
                principalSchema: "vault",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBalances_Accounts_AccountId",
                schema: "vault",
                table: "AccountBalances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountBalances",
                schema: "vault",
                table: "AccountBalances");

            migrationBuilder.RenameTable(
                name: "AccountBalances",
                schema: "vault",
                newName: "BalanceHistory",
                newSchema: "vault");

            migrationBuilder.RenameIndex(
                name: "IX_AccountBalances_AccountId",
                schema: "vault",
                table: "BalanceHistory",
                newName: "IX_BalanceHistory_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BalanceHistory",
                schema: "vault",
                table: "BalanceHistory",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"),
                column: "OwnerId",
                value: "158A357A-0495-4C43-850E-20AFB1F365E3");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("3e28b304-faa8-4942-8c9b-4cf6fb6c621c"),
                column: "OwnerId",
                value: "158A357A-0495-4C43-850E-20AFB1F365E3");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("61b49ed2-7b6a-46b3-bd20-efcbc914bcb1"),
                column: "OwnerId",
                value: "158A357A-0495-4C43-850E-20AFB1F365E3");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("769dd368-4d01-4aa1-80c7-17b5a82793bb"),
                column: "OwnerId",
                value: "158A357A-0495-4C43-850E-20AFB1F365E3");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "BalanceHistory",
                keyColumn: "Id",
                keyValue: new Guid("2be0a47b-63ca-4831-8f88-c78b4a041f3a"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(774), new DateTime(2021, 8, 2, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(780), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(775) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "BalanceHistory",
                keyColumn: "Id",
                keyValue: new Guid("2f778481-e5e4-4199-bef7-7eff404090e2"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 18, 38, 32, 894, DateTimeKind.Utc).AddTicks(7054), new DateTime(2021, 8, 4, 18, 38, 32, 894, DateTimeKind.Utc).AddTicks(8704), new DateTime(2021, 8, 4, 18, 38, 32, 894, DateTimeKind.Utc).AddTicks(7070) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "BalanceHistory",
                keyColumn: "Id",
                keyValue: new Guid("d2cfab5b-dabd-47f7-a1b4-cf4d92d51eb2"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(781), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(786), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(782) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "BalanceHistory",
                keyColumn: "Id",
                keyValue: new Guid("eeab52ea-f443-4e28-8a6f-65de9ee231a2"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(727), new DateTime(2021, 8, 2, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(743), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(730) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "BalanceHistory",
                keyColumn: "Id",
                keyValue: new Guid("ef657a9c-2862-46e2-8de0-777234b8eed4"),
                columns: new[] { "CreatedAt", "EntryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(787), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(791), new DateTime(2021, 8, 4, 18, 38, 32, 895, DateTimeKind.Utc).AddTicks(788) });

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceHistory_Accounts_AccountId",
                schema: "vault",
                table: "BalanceHistory",
                column: "AccountId",
                principalSchema: "vault",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
