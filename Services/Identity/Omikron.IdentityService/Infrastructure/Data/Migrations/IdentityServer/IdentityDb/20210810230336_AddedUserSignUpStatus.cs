using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AddedUserSignUpStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SignUpStatus",
                schema: "usm",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "088200de69864fe882a287dd1e5a76b5");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ac8f7abdf8d742f0b79421373b672a8e");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "43a9188b899e4509b9b727f5e45a8a65");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "5f14f30035df443dad26a91bf5ff9ece");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(425), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(436) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2573), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2576) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2590), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2591) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2596), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2597) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2601), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2602) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2610), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2611) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2614), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2615) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2619), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2623), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2624) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2629), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2633), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2634) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2637), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2638) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2642), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2642) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2646), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2647) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2650), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2651) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2654), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2655) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2659), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2665), new DateTime(2021, 8, 10, 23, 3, 36, 135, DateTimeKind.Utc).AddTicks(2666) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 8, 10, 23, 3, 36, 142, DateTimeKind.Utc).AddTicks(6344), new DateTime(2021, 8, 10, 23, 3, 36, 142, DateTimeKind.Utc).AddTicks(6360), new DateTime(2021, 8, 10, 23, 3, 36, 142, DateTimeKind.Utc).AddTicks(9016) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignUpStatus",
                schema: "usm",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b109ecd1c9844b74b90b1ea2554ce808");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "027ad99bc6014356958ffd1cb109d2ef");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4a84dfdea2284e139834a5a50452bf16");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "ac65b727f1db4bfa9aa5c8e7d48f59ad");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(6305), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(6318) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7823), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7825) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7834), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7834) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7838), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7839) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7843), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7844) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7852), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7852) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7856), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7857) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7860), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7861) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7864), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7865) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7869), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7870) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7874), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7874) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7878), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7879) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7882), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7883) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7886), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7887) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7890), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7891) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7894), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7894) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7898), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7903), new DateTime(2021, 7, 24, 5, 44, 8, 206, DateTimeKind.Utc).AddTicks(7904) });

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: new Guid("7b84cbc0-8e92-408c-8703-d47da88bcb66"),
                columns: new[] { "CreatedAt", "ModifiedAt", "TokenCreationTime" },
                values: new object[] { new DateTime(2021, 7, 24, 5, 44, 8, 213, DateTimeKind.Utc).AddTicks(667), new DateTime(2021, 7, 24, 5, 44, 8, 213, DateTimeKind.Utc).AddTicks(678), new DateTime(2021, 7, 24, 5, 44, 8, 213, DateTimeKind.Utc).AddTicks(1776) });
        }
    }
}
