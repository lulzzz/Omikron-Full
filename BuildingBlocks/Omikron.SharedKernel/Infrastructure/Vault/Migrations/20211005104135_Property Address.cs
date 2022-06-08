using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.SharedKernel.Infrastructure.Vault.Migrations
{
    public partial class PropertyAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("13c3b812-2e3c-47a1-95d7-3556791f31b4"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("156f8a08-a093-4114-bdfd-715c17f89841"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("1f91b8b3-7df2-4068-8ce6-1b6b86112ef5"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("20c59d43-fbbd-449e-bc1c-8ece9ac666b9"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("5a057d54-d369-40c7-8b25-fe96b237823d"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("74dda12d-478f-467c-97af-80d690066a12"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("85fc8125-dc53-40c3-9504-ce78477c7c0f"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("9c155178-ab4d-405b-8ad3-a0e4f622946c"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("a4015da5-dc62-4331-9578-b1ca299fb6c8"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("a8a4f471-71e3-421b-b6c2-5f9468ec420d"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("bd84ec24-74df-46bc-9284-73c40b63f05a"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("e8a34b63-40f3-4ef0-865a-cebff41117fd"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("ea2d7f80-e27b-42ea-8fac-ba84b670ca2d"));

            migrationBuilder.InsertData(
                schema: "vault",
                table: "CategoryIcons",
                columns: new[] { "Id", "Category", "CreatedAt", "Icon", "ModifiedAt" },
                values: new object[,]
                {
                    { new Guid("ad5c53c4-1683-4fbd-bc34-bf0fc592b9e9"), "bills", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(2201), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M19 22H5a3 3 0 0 1-3-3V3a1 1 0 0 1 1-1h14a1 1 0 0 1 1 1v12h4v4a3 3 0 0 1-3 3zm-1-5v2a1 1 0 0 0 2 0v-2h-2zm-2 3V4H4v15a1 1 0 0 0 1 1h11zM6 7h8v2H6V7zm0 4h8v2H6v-2zm0 4h5v2H6v-2z' fill='rgba(107,213,225,1)'/></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(2208) },
                    { new Guid("bafb8541-aaba-4cee-85fa-7a5d859cb22d"), "eating_out", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3174), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><<path fill='none' d='M0 0h24v24H0z'/><path d='M14.268 12.146l-.854.854 7.071 7.071-1.414 1.414L12 14.415l-7.071 7.07-1.414-1.414 9.339-9.339c-.588-1.457.02-3.555 1.62-5.157 1.953-1.952 4.644-2.427 6.011-1.06s.892 4.058-1.06 6.01c-1.602 1.602-3.7 2.21-5.157 1.621zM4.222 3.808l6.717 6.717-2.828 2.829-3.89-3.89a4 4 0 0 1 0-5.656zM18.01 9.11c1.258-1.257 1.517-2.726 1.061-3.182-.456-.456-1.925-.197-3.182 1.06-1.257 1.258-1.516 2.727-1.06 3.183.455.455 1.924.196 3.181-1.061z' fill='rgba(255, 124, 163, 1)'/></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3177) },
                    { new Guid("9ea6cfd2-a14d-49ca-8309-c441a62aa748"), "entertainment", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3182), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M2 3.993A1 1 0 0 1 2.992 3h18.016c.548 0 .992.445.992.993v16.014a1 1 0 0 1-.992.993H2.992A.993.993 0 0 1 2 20.007V3.993zM8 5v14h8V5H8zM4 5v2h2V5H4zm14 0v2h2V5h-2zM4 9v2h2V9H4zm14 0v2h2V9h-2zM4 13v2h2v-2H4zm14 0v2h2v-2h-2zM4 17v2h2v-2H4zm14 0v2h2v-2h-2z' fill='rgba(183, 107, 242, 1)' /></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3182) },
                    { new Guid("97ee6afd-25ac-4976-99a0-f0cf7aca493a"), "expenses", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3185), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-2a8 8 0 1 0 0-16 8 8 0 0 0 0 16zm-3.5-6H14a.5.5 0 1 0 0-1h-4a2.5 2.5 0 1 1 0-5h1V6h2v2h2.5v2H10a.5.5 0 1 0 0 1h4a2.5 2.5 0 1 1 0 5h-1v2h-2v-2H8.5v-2z' fill='rgba(255, 88, 88, 1)' /></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3186) },
                    { new Guid("1df36ed6-1e3e-40c5-ae11-4eb238d471ac"), "general", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3188), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M20 7V5H4v14h16v-2h-8a1 1 0 0 1-1-1V8a1 1 0 0 1 1-1h8zM3 3h18a1 1 0 0 1 1 1v16a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1V4a1 1 0 0 1 1-1zm10 6v6h7V9h-7zm2 2h3v2h-3v-2z' fill='rgba(112, 199, 152, 1)' /></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3189) },
                    { new Guid("b86202a2-ccc0-4a29-8ed7-4204f6f64b92"), "groceries", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3190), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M20 22H4a1 1 0 0 1-1-1V3a1 1 0 0 1 1-1h16a1 1 0 0 1 1 1v18a1 1 0 0 1-1 1zm-1-2V4H5v16h14zM9 6v2a3 3 0 0 0 6 0V6h2v2A5 5 0 0 1 7 8V6h2z' fill='rgba(255,193,74,1)'/></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3191) },
                    { new Guid("5b665233-2510-44f6-968c-138eeb4d14b7"), "holidays", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3193), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0H24V24H0z'/><path d='M18 23h-2v-1H8v1H6v-1H5c-1.105 0-2-.895-2-2V7c0-1.105.895-2 2-2h3V3c0-.552.448-1 1-1h6c.552 0 1 .448 1 1v2h3c1.105 0 2 .895 2 2v13c0 1.105-.895 2-2 2h-1v1zm1-16H5v13h14V7zm-9 2v9H8V9h2zm6 0v9h-2V9h2zm-2-5h-4v1h4V4z' fill='rgba(162, 221, 255, 1)' /></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3193) },
                    { new Guid("15ab0b36-df47-45b8-a3d6-db65395d9d46"), "internal", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3201), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 20a8 8 0 1 0 0-16 8 8 0 0 0 0 16zm0 2C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-6a4 4 0 1 0 0-8 4 4 0 0 0 0 8zm0 2a6 6 0 1 1 0-12 6 6 0 0 1 0 12zm0-4a2 2 0 1 1 0-4 2 2 0 0 1 0 4z' fill='rgba(255, 150, 74, 1)' /></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3202) },
                    { new Guid("93ef1274-510d-40af-9323-47e5c8f3fcb0"), "personal_care", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3203), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M17.841 15.659l.176.177.178-.177a2.25 2.25 0 0 1 3.182 3.182l-3.36 3.359-3.358-3.359a2.25 2.25 0 0 1 3.182-3.182zM12 14v2a6 6 0 0 0-6 6H4a8 8 0 0 1 7.75-7.996L12 14zm0-13c3.315 0 6 2.685 6 6a5.998 5.998 0 0 1-5.775 5.996L12 13c-3.315 0-6-2.685-6-6a5.998 5.998 0 0 1 5.775-5.996L12 1zm0 2C9.79 3 8 4.79 8 7s1.79 4 4 4 4-1.79 4-4-1.79-4-4-4z' fill='rgba(236, 96, 197, 1)' /></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3204) },
                    { new Guid("7c56f87c-989a-4d75-932e-14bf725f4446"), "personal_finance", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3206), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M9.33 11.5h2.17A4.5 4.5 0 0 1 16 16H8.999L9 17h8v-1a5.578 5.578 0 0 0-.886-3H19a5 5 0 0 1 4.516 2.851C21.151 18.972 17.322 21 13 21c-2.761 0-5.1-.59-7-1.625L6 10.071A6.967 6.967 0 0 1 9.33 11.5zM5 19a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1v-9a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v9zM18 5a3 3 0 1 1 0 6 3 3 0 0 1 0-6zm-7-3a3 3 0 1 1 0 6 3 3 0 0 1 0-6z' fill='rgba(142, 205, 120, 1)' /></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3207) },
                    { new Guid("c60dabe7-7d91-4112-a2ae-26698c81258a"), "shopping", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3208), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 2a6 6 0 0 1 6 6v1h4v2h-1.167l-.757 9.083a1 1 0 0 1-.996.917H4.92a1 1 0 0 1-.996-.917L3.166 11H2V9h4V8a6 6 0 0 1 6-6zm6.826 9H5.173l.667 8h12.319l.667-8zM13 13v4h-2v-4h2zm-4 0v4H7v-4h2zm8 0v4h-2v-4h2zm-5-9a4 4 0 0 0-3.995 3.8L8 8v1h8V8a4 4 0 0 0-3.8-3.995L12 4z' fill='rgba(149,138,248,1)'/></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3209) },
                    { new Guid("89868a71-c920-4230-b9c8-8d981049f674"), "transport", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3211), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M19 20H5v1a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1v-9l2.513-6.702A2 2 0 0 1 6.386 4h11.228a2 2 0 0 1 1.873 1.298L22 12v9a1 1 0 0 1-1 1h-1a1 1 0 0 1-1-1v-1zM4.136 12h15.728l-2.25-6H6.386l-2.25 6zM6.5 17a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3zm11 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z' fill='rgba(75, 141, 218, 1)' /></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3211) },
                    { new Guid("47f16167-4fcc-4a6e-9898-f4f0acbb5037"), "finances", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3213), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm-3.5-8v2H11v2h2v-2h1a2.5 2.5 0 1 0 0-5h-4a.5.5 0 1 1 0-1h5.5V8H13V6h-2v2h-1a2.5 2.5 0 0 0 0 5h4a.5.5 0 1 1 0 1H8.5z' fill='rgba(50, 182, 122, 1)' /></svg>", new DateTime(2021, 10, 5, 10, 41, 35, 417, DateTimeKind.Utc).AddTicks(3213) }
                });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 5, 10, 41, 35, 441, DateTimeKind.Utc).AddTicks(1881), new DateTime(2021, 10, 5, 10, 41, 35, 441, DateTimeKind.Utc).AddTicks(1887) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Merchants_MerchantId",
                schema: "vault",
                table: "Transactions");

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("15ab0b36-df47-45b8-a3d6-db65395d9d46"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("1df36ed6-1e3e-40c5-ae11-4eb238d471ac"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("47f16167-4fcc-4a6e-9898-f4f0acbb5037"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("5b665233-2510-44f6-968c-138eeb4d14b7"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("7c56f87c-989a-4d75-932e-14bf725f4446"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("89868a71-c920-4230-b9c8-8d981049f674"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("93ef1274-510d-40af-9323-47e5c8f3fcb0"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("97ee6afd-25ac-4976-99a0-f0cf7aca493a"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("9ea6cfd2-a14d-49ca-8309-c441a62aa748"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("ad5c53c4-1683-4fbd-bc34-bf0fc592b9e9"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("b86202a2-ccc0-4a29-8ed7-4204f6f64b92"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("bafb8541-aaba-4cee-85fa-7a5d859cb22d"));

            migrationBuilder.DeleteData(
                schema: "vault",
                table: "CategoryIcons",
                keyColumn: "Id",
                keyValue: new Guid("c60dabe7-7d91-4112-a2ae-26698c81258a"));

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "vault",
                table: "Properties");

            migrationBuilder.InsertData(
                schema: "vault",
                table: "CategoryIcons",
                columns: new[] { "Id", "Category", "CreatedAt", "Icon", "ModifiedAt" },
                values: new object[,]
                {
                    { new Guid("a4015da5-dc62-4331-9578-b1ca299fb6c8"), "bills", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(2435), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M19 22H5a3 3 0 0 1-3-3V3a1 1 0 0 1 1-1h14a1 1 0 0 1 1 1v12h4v4a3 3 0 0 1-3 3zm-1-5v2a1 1 0 0 0 2 0v-2h-2zm-2 3V4H4v15a1 1 0 0 0 1 1h11zM6 7h8v2H6V7zm0 4h8v2H6v-2zm0 4h5v2H6v-2z' fill='rgba(107,213,225,1)'/></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(2446) },
                    { new Guid("e8a34b63-40f3-4ef0-865a-cebff41117fd"), "eating_out", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3560), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><<path fill='none' d='M0 0h24v24H0z'/><path d='M14.268 12.146l-.854.854 7.071 7.071-1.414 1.414L12 14.415l-7.071 7.07-1.414-1.414 9.339-9.339c-.588-1.457.02-3.555 1.62-5.157 1.953-1.952 4.644-2.427 6.011-1.06s.892 4.058-1.06 6.01c-1.602 1.602-3.7 2.21-5.157 1.621zM4.222 3.808l6.717 6.717-2.828 2.829-3.89-3.89a4 4 0 0 1 0-5.656zM18.01 9.11c1.258-1.257 1.517-2.726 1.061-3.182-.456-.456-1.925-.197-3.182 1.06-1.257 1.258-1.516 2.727-1.06 3.183.455.455 1.924.196 3.181-1.061z' fill='rgba(255, 124, 163, 1)'/></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3563) },
                    { new Guid("85fc8125-dc53-40c3-9504-ce78477c7c0f"), "entertainment", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3569), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M2 3.993A1 1 0 0 1 2.992 3h18.016c.548 0 .992.445.992.993v16.014a1 1 0 0 1-.992.993H2.992A.993.993 0 0 1 2 20.007V3.993zM8 5v14h8V5H8zM4 5v2h2V5H4zm14 0v2h2V5h-2zM4 9v2h2V9H4zm14 0v2h2V9h-2zM4 13v2h2v-2H4zm14 0v2h2v-2h-2zM4 17v2h2v-2H4zm14 0v2h2v-2h-2z' fill='rgba(183, 107, 242, 1)' /></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3570) },
                    { new Guid("156f8a08-a093-4114-bdfd-715c17f89841"), "expenses", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3572), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-2a8 8 0 1 0 0-16 8 8 0 0 0 0 16zm-3.5-6H14a.5.5 0 1 0 0-1h-4a2.5 2.5 0 1 1 0-5h1V6h2v2h2.5v2H10a.5.5 0 1 0 0 1h4a2.5 2.5 0 1 1 0 5h-1v2h-2v-2H8.5v-2z' fill='rgba(255, 88, 88, 1)' /></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3573) },
                    { new Guid("74dda12d-478f-467c-97af-80d690066a12"), "general", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3574), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M20 7V5H4v14h16v-2h-8a1 1 0 0 1-1-1V8a1 1 0 0 1 1-1h8zM3 3h18a1 1 0 0 1 1 1v16a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1V4a1 1 0 0 1 1-1zm10 6v6h7V9h-7zm2 2h3v2h-3v-2z' fill='rgba(112, 199, 152, 1)' /></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3575) },
                    { new Guid("9c155178-ab4d-405b-8ad3-a0e4f622946c"), "groceries", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3576), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M20 22H4a1 1 0 0 1-1-1V3a1 1 0 0 1 1-1h16a1 1 0 0 1 1 1v18a1 1 0 0 1-1 1zm-1-2V4H5v16h14zM9 6v2a3 3 0 0 0 6 0V6h2v2A5 5 0 0 1 7 8V6h2z' fill='rgba(255,193,74,1)'/></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3577) },
                    { new Guid("ea2d7f80-e27b-42ea-8fac-ba84b670ca2d"), "holidays", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3578), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0H24V24H0z'/><path d='M18 23h-2v-1H8v1H6v-1H5c-1.105 0-2-.895-2-2V7c0-1.105.895-2 2-2h3V3c0-.552.448-1 1-1h6c.552 0 1 .448 1 1v2h3c1.105 0 2 .895 2 2v13c0 1.105-.895 2-2 2h-1v1zm1-16H5v13h14V7zm-9 2v9H8V9h2zm6 0v9h-2V9h2zm-2-5h-4v1h4V4z' fill='rgba(162, 221, 255, 1)' /></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3579) },
                    { new Guid("1f91b8b3-7df2-4068-8ce6-1b6b86112ef5"), "internal", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3581), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 20a8 8 0 1 0 0-16 8 8 0 0 0 0 16zm0 2C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm0-6a4 4 0 1 0 0-8 4 4 0 0 0 0 8zm0 2a6 6 0 1 1 0-12 6 6 0 0 1 0 12zm0-4a2 2 0 1 1 0-4 2 2 0 0 1 0 4z' fill='rgba(255, 150, 74, 1)' /></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3581) },
                    { new Guid("a8a4f471-71e3-421b-b6c2-5f9468ec420d"), "personal_care", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3583), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M17.841 15.659l.176.177.178-.177a2.25 2.25 0 0 1 3.182 3.182l-3.36 3.359-3.358-3.359a2.25 2.25 0 0 1 3.182-3.182zM12 14v2a6 6 0 0 0-6 6H4a8 8 0 0 1 7.75-7.996L12 14zm0-13c3.315 0 6 2.685 6 6a5.998 5.998 0 0 1-5.775 5.996L12 13c-3.315 0-6-2.685-6-6a5.998 5.998 0 0 1 5.775-5.996L12 1zm0 2C9.79 3 8 4.79 8 7s1.79 4 4 4 4-1.79 4-4-1.79-4-4-4z' fill='rgba(236, 96, 197, 1)' /></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3584) },
                    { new Guid("5a057d54-d369-40c7-8b25-fe96b237823d"), "personal_finance", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3589), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M9.33 11.5h2.17A4.5 4.5 0 0 1 16 16H8.999L9 17h8v-1a5.578 5.578 0 0 0-.886-3H19a5 5 0 0 1 4.516 2.851C21.151 18.972 17.322 21 13 21c-2.761 0-5.1-.59-7-1.625L6 10.071A6.967 6.967 0 0 1 9.33 11.5zM5 19a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1v-9a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v9zM18 5a3 3 0 1 1 0 6 3 3 0 0 1 0-6zm-7-3a3 3 0 1 1 0 6 3 3 0 0 1 0-6z' fill='rgba(142, 205, 120, 1)' /></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3590) },
                    { new Guid("bd84ec24-74df-46bc-9284-73c40b63f05a"), "shopping", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3592), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 2a6 6 0 0 1 6 6v1h4v2h-1.167l-.757 9.083a1 1 0 0 1-.996.917H4.92a1 1 0 0 1-.996-.917L3.166 11H2V9h4V8a6 6 0 0 1 6-6zm6.826 9H5.173l.667 8h12.319l.667-8zM13 13v4h-2v-4h2zm-4 0v4H7v-4h2zm8 0v4h-2v-4h2zm-5-9a4 4 0 0 0-3.995 3.8L8 8v1h8V8a4 4 0 0 0-3.8-3.995L12 4z' fill='rgba(149,138,248,1)'/></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3592) },
                    { new Guid("13c3b812-2e3c-47a1-95d7-3556791f31b4"), "transport", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3594), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M19 20H5v1a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1v-9l2.513-6.702A2 2 0 0 1 6.386 4h11.228a2 2 0 0 1 1.873 1.298L22 12v9a1 1 0 0 1-1 1h-1a1 1 0 0 1-1-1v-1zM4.136 12h15.728l-2.25-6H6.386l-2.25 6zM6.5 17a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3zm11 0a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z' fill='rgba(75, 141, 218, 1)' /></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3595) },
                    { new Guid("20c59d43-fbbd-449e-bc1c-8ece9ac666b9"), "finances", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3596), "<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='32' height='32'><path fill='none' d='M0 0h24v24H0z'/><path d='M12 22C6.477 22 2 17.523 2 12S6.477 2 12 2s10 4.477 10 10-4.477 10-10 10zm-3.5-8v2H11v2h2v-2h1a2.5 2.5 0 1 0 0-5h-4a.5.5 0 1 1 0-1h5.5V8H13V6h-2v2h-1a2.5 2.5 0 0 0 0 5h4a.5.5 0 1 1 0 1H8.5z' fill='rgba(50, 182, 122, 1)' /></svg>", new DateTime(2021, 9, 30, 13, 57, 29, 863, DateTimeKind.Utc).AddTicks(3597) }
                });

            migrationBuilder.UpdateData(
                schema: "vault",
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "3338F41E-3AD7-46D7-B8A6-869CBEFA2BE8",
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 10, 1, 11, 33, 4, 446, DateTimeKind.Utc).AddTicks(8890), new DateTime(2021, 10, 1, 11, 33, 4, 446, DateTimeKind.Utc).AddTicks(8896) });
        }
    }
}
