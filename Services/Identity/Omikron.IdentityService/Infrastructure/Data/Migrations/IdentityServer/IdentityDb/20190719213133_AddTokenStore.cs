using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddTokenStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetPasswordTokenExpiration",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ConfirmationTokens",
                schema: "usm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Expiration = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    TenantId = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfirmationTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "usm",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5f53208e6233485d8b974af0ea96c21d");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmationTokens_UserId",
                schema: "usm",
                table: "ConfirmationTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmationTokens",
                schema: "usm");

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                schema: "usm",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordTokenExpiration",
                schema: "usm",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "3d4c7677808b47b89b4cd0f66e56ddba");
        }
    }
}
