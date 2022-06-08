using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddAccountsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "vault",
                table: "Accounts",
                columns: new[] { "Id", "AccountSource", "CreatedAt", "ModifiedAt", "OwnerId" },
                values: new object[] { new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"), 1, new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), new DateTime(2021, 8, 1, 17, 55, 0, 0, DateTimeKind.Utc), "158A357A-0495-4C43-850E-20AFB1F365E3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "vault",
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0679fd05-4c37-4eec-a029-62138d8e82b1"));
        }
    }
}
