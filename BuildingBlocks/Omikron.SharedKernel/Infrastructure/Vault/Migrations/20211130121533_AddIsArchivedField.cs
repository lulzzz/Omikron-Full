using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class AddIsArchivedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "vault",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "vault",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "vault",
                table: "PersonalItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "vault",
                table: "Investments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "vault",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "vault",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "vault",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "vault",
                table: "PersonalItems");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "vault",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "vault",
                table: "Accounts");
        }
    }
}
