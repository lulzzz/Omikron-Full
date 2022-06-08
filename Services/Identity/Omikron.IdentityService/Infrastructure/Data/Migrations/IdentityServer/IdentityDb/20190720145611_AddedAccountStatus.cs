using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddedAccountStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountStatus",
                schema: "usm",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "66a150606380436da646dcb65a1cab93");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountStatus",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7168b1ab00734ed8b64af065453109c4");
        }
    }
}
