using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class PersonalItemModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "vault",
                table: "PersonalItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("48ab7b04-218d-4205-a177-437db3d0a552"),
                columns: new[] { "CreatedAt", "ExpiryDate", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 59, 11, 601, DateTimeKind.Utc).AddTicks(4764), new DateTime(2021, 9, 12, 20, 59, 41, 603, DateTimeKind.Utc).AddTicks(3597), new DateTime(2021, 9, 12, 20, 59, 11, 601, DateTimeKind.Utc).AddTicks(4777) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("6a18dcff-d1f0-4067-bcd9-fc13c863e3c3"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(4812), new DateTime(2021, 9, 10, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(3356), new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(4813) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("93024c9c-013b-4635-82ba-842f4067f8f9"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(3664), new DateTime(2021, 9, 10, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(3356), new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(3665) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("a7a9dfc3-67c2-401c-b436-74888bd9bf54"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(4826), new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(3356), new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(4827) });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: new Guid("ea759c8f-6ee4-4231-83c0-27211c50f1ca"),
                columns: new[] { "CreatedAt", "Date", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(4822), new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(3356), new DateTime(2021, 9, 12, 20, 59, 11, 606, DateTimeKind.Utc).AddTicks(4823) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "vault",
                table: "PersonalItems");

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
        }
    }
}
