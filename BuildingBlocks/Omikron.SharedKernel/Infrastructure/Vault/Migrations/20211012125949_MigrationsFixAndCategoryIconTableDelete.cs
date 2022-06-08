using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class MigrationsFixAndCategoryIconTableDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(schema: "vault",
                name: "Address",
                table: "Properties",
                type: "nvarchar(max)"
            );

            migrationBuilder.DropTable(
                name: "CategoryIcons",
                schema: "vault");

            migrationBuilder.UpdateData(
                 schema: "vault",
                 table: "Tenant",
                 keyColumn: "Id",
                 keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                 columns: new[] { "CreatedAt", "ModifiedAt" },
                 values: new object[] { new DateTime(2021, 10, 12, 12, 59, 49, 441, DateTimeKind.Utc).AddTicks(3138), new DateTime(2021, 10, 12, 12, 59, 49, 441, DateTimeKind.Utc).AddTicks(3151) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(schema: "vault", table: "Properties", name: "Address");

            migrationBuilder.CreateTable(
                name: "CategoryIcons",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryIcons", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "vault",
                table: "CategoryIcons",
                columns: new[] { "Id", "Category", "CreatedAt", "Icon", "ModifiedAt" },
                values: new object[,]
                {
                    { new Guid("09bc8713-9d56-41d6-a9a3-9cd2a689eee6"), "bills", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(188), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M19 22H5a3 3 0 0 1-3-3V3a1 1 0 0 1 1-1h14a1 1 0 0 1 1 1v12h4v4a3 3 0 0 1-3 3zm-1-5v2a1 1 0 0 0 2 0v-2h-2zm-2 3V4H4v15a1 1 0 0 0 1 1h11zM6 7h8v2H6V7zm0 4h8v2H6v-2zm0 4h5v2H6v-2z' fill='rgba(107,213,225,1)'/></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(199) },
                    { new Guid("8ca96aed-df32-4051-bf6d-9815216e4f50"), "eating_out", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1366), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><<path fill='none' d='M0 0h24v24H0z'/><path d='M14.268 12.146l-.854.854 7.071 7.071-1.414 1.414L12 14.415l-7.071 7.07-1.414-1.414 9.339-9.339c-.588-1.457.02-3.555 1.62-5.157 1.953-1.952 4.644-2.427 6.011-1.06s.892 4.058-1.06 6.01c-1.602 1.602-3.7 2.21-5.157 1.621zM4.222 3.808l6.717 6.717-2.828 2.829-3.89-3.89a4 4 0 0 1 0-5.656zM18.01 9.11c1.258-1.257 1.517-2.726 1.061-3.182-.456-.456-1.925-.197-3.182 1.06-1.257 1.258-1.516 2.727-1.06 3.183.455.455 1.924.196 3.181-1.061z' fill='rgba(255, 124, 163, 1)'/></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1367) },
                    { new Guid("1e71db13-a834-4fe4-9e4f-76ef01f8f8da"), "entertainment", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1375), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M2 3.993A1 1 0 0 1 2.992 3h18.016c.548 0 .992.445.992.993v16.014a1 1 0 0 1-.992.993H2.992A.993.993 0 0 1 2 20.007V3.993zM8 5v14h8V5H8zM4 5v2h2V5H4zm14 0v2h2V5h-2zM4 9v2h2V9H4zm14 0v2h2V9h-2zM4 13v2h2v-2H4zm14 0v2h2v-2h-2zM4 17v2h2v-2H4zm14 0v2h2v-2h-2z' fill='rgba(183, 107, 242, 1)' /></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1376) },
                    { new Guid("f240732a-7cfc-4c2a-b50d-57c8c731c0a3"), "expenses", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1377), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-2a8 8 0 1 0 0-16 8 8 0 0 0 0 16zm-3.5-6H14a.5.5 0 1 0 0-1h-4a2.5 2.5 0 1 1 0-5h1V6h2v2h2.5v2H10a.5.5 0 1 0 0 1h4a2.5 2.5 0 1 1 0 5h-1v2h-2v-2H8.5v-2z' fill='rgba(255, 88, 88, 1)' /></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1378) },
                    { new Guid("7c94c21d-dc6e-4d9d-8a23-b46bc6cdc0b3"), "general", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1380), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M20 7V5H4v14h16v-2h-8a1 1 0 0 1-1-1V8a1 1 0 0 1 1-1h8zM3 3h18a1 1 0 0 1 1 1v16a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1V4a1 1 0 0 1 1-1zm10 6v6h7V9h-7zm2 2h3v2h-3v-2z' fill='rgba(112, 199, 152, 1)' /></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1380) },
                    { new Guid("277fa761-887f-4314-b20f-99635b14b196"), "groceries", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1382), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M20 22H4a1 1 0 0 1-1-1V3a1 1 0 0 1 1-1h16a1 1 0 0 1 1 1v18a1 1 0 0 1-1 1zm-1-2V4H5v16h14zM9 6v2a3 3 0 0 0 6 0V6h2v2A5 5 0 0 1 7 8V6h2z' fill='rgba(255,193,74,1)'/></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1383) },
                    { new Guid("a9c839e1-7a76-4195-9230-44104063c7e0"), "holidays", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1384), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0H24V24H0z'/><path d='M18 23h-2v-1H8v1H6v-1H5c-1.105 0-2-.895-2-2V7c0-1.105.895-2 2-2h3V3c0-.552.448-1 1-1h6c.552 0 1 .448 1 1v2h3c1.105 0 2 .895 2 2v13c0 1.105-.895 2-2 2h-1v1zm1-16H5v13h14V7zm-9 2v9H8V9h2zm6 0v9h-2V9h2zm-2-5h-4v1h4V4z' fill='rgba(162, 221, 255, 1)' /></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1385) },
                    { new Guid("3399704e-7df3-4783-ac55-6788b12c908a"), "internal", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1387), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 20a8 8 0 1 0 0-16 8 8 0 0 0 0 16zm0 2C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-6a4 4 0 1 0 0-8 4 4 0 0 0 0 8zm0 2a6 6 0 1 1 0-12 6 6 0 0 1 0 12zm0-4a2 2 0 1 1 0-4 2 2 0 0 1 0 4z' fill='rgba(255, 150, 74, 1)' /></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1388) },
                    { new Guid("3c31a15a-493e-4650-badb-75db70eed2eb"), "personal_care", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1389), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M17.841 15.659l.176.177.178-.177a2.25 2.25 0 0 1 3.182 3.182l-3.36 3.359-3.358-3.359a2.25 2.25 0 0 1 3.182-3.182zM12 14v2a6 6 0 0 0-6 6H4a8 8 0 0 1 7.75-7.996L12 14zm0-13c3.315 0 6 2.685 6 6a5.998 5.998 0 0 1-5.775 5.996L12 13c-3.315 0-6-2.685-6-6a5.998 5.998 0 0 1 5.775-5.996L12 1zm0 2C9.79 3 8 4.79 8 7s1.79 4 4 4 4-1.79 4-4-1.79-4-4-4z' fill='rgba(236, 96, 197, 1)' /></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1390) },
                    { new Guid("e0eac8f5-c938-46f3-9367-b7127ac58f63"), "personal_finance", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1395), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M9.33 11.5h2.17A4.5 4.5 0 0 1 16 16H8.999L9 17h8v-1a5.578 5.578 0 0 0-.886-3H19a5 5 0 0 1 4.516 2.851C21.151 18.972 17.322 21 13 21c-2.761 0-5.1-.59-7-1.625L6 10.071A6.967 6.967 0 0 1 9.33 11.5zM5 19a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1v-9a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v9zM18 5a3 3 0 1 1 0 6 3 3 0 0 1 0-6zm-7-3a3 3 0 1 1 0 6 3 3 0 0 1 0-6z' fill='rgba(142, 205, 120, 1)' /></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1396) },
                    { new Guid("7b40d2d0-0cc6-4294-ad09-875382eb4bb8"), "shopping", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1397), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 2a6 6 0 0 1 6 6v1h4v2h-1.167l-.757 9.083a1 1 0 0 1-.996.917H4.92a1 1 0 0 1-.996-.917L3.166 11H2V9h4V8a6 6 0 0 1 6-6zm6.826 9H5.173l.667 8h12.319l.667-8zM13 13v4h-2v-4h2zm-4 0v4H7v-4h2zm8 0v4h-2v-4h2zm-5-9a4 4 0 0 0-3.995 3.8L8 8v1h8V8a4 4 0 0 0-3.8-3.995L12 4z' fill='rgba(149,138,248,1)'/></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1398) },
                    { new Guid("f390dbfc-7d96-4a89-a0de-5f92f17d5e5d"), "transport", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1400), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M19 20H5v1a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1v-9l2.513-6.702A2 2 0 0 1 6.386 4h11.228a2 2 0 0 1 1.873 1.298L22 12v9a1 1 0 0 1-1 1h-1a1 1 0 0 1-1-1v-1zM4.136 12h15.728l-2.25-6H6.386l-2.25 6zM6.5 17a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3zm11 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z' fill='rgba(75, 141, 218, 1)' /></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1400) },
                    { new Guid("5086b1ab-43e6-4654-81dc-21e038617666"), "finances", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1402), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm-3.5-8v2H11v2h2v-2h1a2.5 2.5 0 1 0 0-5h-4a.5.5 0 1 1 0-1h5.5V8H13V6h-2v2h-1a2.5 2.5 0 0 0 0 5h4a.5.5 0 1 1 0 1H8.5z' fill='rgba(50, 182, 122, 1)' /></svg>", new DateTime(2021, 10, 7, 19, 44, 54, 197, DateTimeKind.Utc).AddTicks(1403) }
                });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 7, 19, 44, 54, 228, DateTimeKind.Utc).AddTicks(2120), new DateTime(2021, 10, 7, 19, 44, 54, 228, DateTimeKind.Utc).AddTicks(2128) });
        }
    }
}
