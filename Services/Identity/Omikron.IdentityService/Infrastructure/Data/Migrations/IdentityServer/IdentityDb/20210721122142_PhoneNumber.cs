using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class PhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "usm",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "usm",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "BudCustomerId",
                schema: "usm",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BudCustomerSecret",
                schema: "usm",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                schema: "usm",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhoneNumberId",
                schema: "usm",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                schema: "usm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Token = table.Column<int>(type: "int", nullable: false),
                    Verified = table.Column<bool>(type: "bit", nullable: false),
                    VerificationAttempts = table.Column<int>(type: "int", nullable: false),
                    LockedOut = table.Column<bool>(type: "bit", nullable: false),
                    LockoutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "b84c88f79f164a7eaebf9f42dbdf8cfe", new Guid("dffa36f1-df2a-419d-9941-a32509a14d69") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "086b538f96b84c64b368374eb4f52859");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1c99bfa0876b4dc4b1333a99a21865b0");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(3678), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(3691) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5720), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5723) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5736), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5737) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5742), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5743) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5747), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5748) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5755), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5756) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5759), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5764), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5765) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5769), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5775), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5776) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5779), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5784), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5785) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5788), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5789) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5793), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5794) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5797), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5798) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5801), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5802) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5807), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5807) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5812), new DateTime(2021, 7, 21, 12, 21, 41, 717, DateTimeKind.Utc).AddTicks(5813) });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhoneNumberId",
                schema: "usm",
                table: "AspNetUsers",
                column: "PhoneNumberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_Number",
                schema: "usm",
                table: "PhoneNumbers",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PhoneNumbers_PhoneNumberId",
                schema: "usm",
                table: "AspNetUsers",
                column: "PhoneNumberId",
                principalSchema: "usm",
                principalTable: "PhoneNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PhoneNumbers_PhoneNumberId",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PhoneNumbers",
                schema: "usm");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhoneNumberId",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BudCustomerId",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BudCustomerSecret",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nickname",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberId",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "usm",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "usm",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "7db26ef1256544859ff8dacdaa5bc680", new Guid("dffa36f1-df2a-419d-9387-a32509a14d69") });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b7bf5399a2434453af1b7bde49565f01");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2f74f518b3964fdebedd45a98c4c8c86");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(1739), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4058), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4061) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4072), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4073) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4076), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4077) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4080), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4081) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4086), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4087) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4090), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4091) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4094), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4094) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4098), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4098) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4102), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4103) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4106), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4107) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4109), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4110) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4113), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4114) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4117), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4117) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4120), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4121) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4125), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4125) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4128), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4129) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4132), new DateTime(2021, 5, 10, 8, 23, 35, 93, DateTimeKind.Utc).AddTicks(4133) });
        }
    }
}
