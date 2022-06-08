using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AutomaticVehicleReValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutomaticallyReValueVehicle",
                schema: "vault",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 29, 9, 4, 8, 318, DateTimeKind.Utc).AddTicks(8794), new DateTime(2021, 9, 29, 9, 4, 8, 318, DateTimeKind.Utc).AddTicks(8801) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutomaticallyReValueVehicle",
                schema: "vault",
                table: "Vehicles");

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 28, 7, 25, 57, 694, DateTimeKind.Utc).AddTicks(6531), new DateTime(2021, 9, 28, 7, 25, 57, 694, DateTimeKind.Utc).AddTicks(6548) });
        }
    }
}
