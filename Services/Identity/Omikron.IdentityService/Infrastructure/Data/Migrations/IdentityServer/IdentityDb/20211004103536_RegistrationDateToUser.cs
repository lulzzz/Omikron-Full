using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class RegistrationDateToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                schema: "usm",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "065e39003e424a9681ee66b731ab3f67");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e9eb6bb5ce3144ad91389872f9721664");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "77584561d992429f97a2bb3c1c287221");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "e10c7a510f824bf9b8a6ba6a17fd668c");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(8202), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(8212) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9464), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9466) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9473), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9473) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9477), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9477) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9481), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9481) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9486), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9487) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9490), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9491) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9494), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9494) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9497), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9497) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9501), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9502) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9504), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9505) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9508), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9508) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9511), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9512) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9514), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9515) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9517), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9518) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9520), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9520) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9523), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9524) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9527), new DateTime(2021, 10, 4, 10, 35, 36, 334, DateTimeKind.Utc).AddTicks(9528) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 10, 4, 10, 35, 36, 341, DateTimeKind.Utc).AddTicks(8470), new DateTime(2021, 10, 4, 10, 35, 36, 341, DateTimeKind.Utc).AddTicks(8563), new DateTime(2021, 10, 4, 10, 35, 36, 341, DateTimeKind.Utc).AddTicks(9614) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d42fbfe789224023ac6987c3700ebfbd");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ec51a9d562ed476fa99b94aee194b025");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ddd445f7c00140cf975ea84642943f27");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "fbf68c0f23d34f0aa1942d27cdaad91c");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 56, DateTimeKind.Utc).AddTicks(8595), new DateTime(2021, 9, 2, 18, 35, 27, 56, DateTimeKind.Utc).AddTicks(8608) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2130), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2146) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2173), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2174) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2181), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2182) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2189), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2190) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2204), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2205) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2212), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2213) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2218), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2219) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2225), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2226) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2239), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2241) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2269), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2271) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2286), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2287) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2294), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2295) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2303), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2304) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2312), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2313) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2401), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2403) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2410), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2411) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2424), new DateTime(2021, 9, 2, 18, 35, 27, 57, DateTimeKind.Utc).AddTicks(2426) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 9, 2, 18, 35, 27, 70, DateTimeKind.Utc).AddTicks(2763), new DateTime(2021, 9, 2, 18, 35, 27, 70, DateTimeKind.Utc).AddTicks(2778), new DateTime(2021, 9, 2, 18, 35, 27, 70, DateTimeKind.Utc).AddTicks(4503) });
        }
    }
}
