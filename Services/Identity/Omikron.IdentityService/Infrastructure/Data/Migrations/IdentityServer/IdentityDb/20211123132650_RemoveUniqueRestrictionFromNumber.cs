using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class RemoveUniqueRestrictionFromNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_Number",
                schema: "usm",
                table: "PhoneNumbers");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_Number",
                schema: "usm",
                table: "PhoneNumbers",
                column: "Number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_Number",
                schema: "usm",
                table: "PhoneNumbers");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_Number",
                schema: "usm",
                table: "PhoneNumbers",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");
        }
    }
}
