using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omikron.IdentityService.Infrastructure.Data.Migrations.IdentityServer.IdentityDb
{
    public partial class AssignedPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "usm",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "usm",
                table: "Permissions");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "aad7b7be72bf42ea8f994c2d460088ae", new Guid("05a075a5-076a-465c-811d-0f5c875e9b3f") });

                migrationBuilder.Sql(
                    @"IF NOT EXISTS (SELECT 1 FROM [usm].[AspNetRoles] WHERE Name = 'System.Tenant.Administrator')
                      BEGIN
                        SET IDENTITY_INSERT[usm].[AspNetRoles] ON
                        INSERT INTO[usm].[AspNetRoles]
                        (Id, Name, NormalizedName, ConcurrencyStamp, ExternalId)
                    VALUES(2, 'System.Tenant.Administrator', 'System.Tenant.Administrator', 'b953e152e59a40348608d82792882b5d', '4e313f82-5801-41ec-b147-86697f85fccf')
                    SET IDENTITY_INSERT[usm].[AspNetRoles] OFF 
                         END");

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 1, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(1143), "Ability to see a page with a list of tenants alongside with search functionality", 1, new Guid("18bb49e7-69ed-47bd-bfc6-5372dd15315a"), null, "Search.Tenants", "Search.Tenants" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 2, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4366), "Ability to see button for add tenant alongside with ability to create tenant", 1, new Guid("9feb92a5-398f-4069-8f5f-6da5212af3f2"), null, "Add.Tenant", "Add.Tenant" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 3, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4512), "Ability to navigate to tenant details page and edit tenant", 1, new Guid("cba862dc-fd5f-4c70-84a6-3fb16cb868e1"), null, "Edit.Tenant", "Edit.Tenant" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 4, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4518), "Ability to navigate to tenant details page without ability to edit tenant", 1, new Guid("865dd083-5872-406a-bf8b-bd440f31d6fe"), null, "Read.Tenant", "Read.Tenant" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 5, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4522), "Ability to see delete action alongside with ability to delete tenant", 1, new Guid("e445a667-0a50-470f-84d5-84959adca363"), null, "Delete.Tenant", "Delete.Tenant" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 6, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4528), "Ability to see a page with a list of users alongside with search functionality", 2, new Guid("8d5e1370-d20c-4e2e-9a93-84f5d4e620c4"), null, "Search.Users", "Search.Users" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 7, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4532), "Ability to see button for add user alongside with ability to create user", 2, new Guid("0ebcb068-fe5b-4696-9392-eca13615139c"), null, "Add.User", "Add.User" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 8, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4535), "Ability to navigate to user details page and edit user", 2, new Guid("9c0d6b32-6f5f-41a3-8336-43f33dd61477"), null, "Edit.User", "Edit.User" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 9, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4540), "Ability to see delete action alongside with ability to delete user", 2, new Guid("d686c12f-8ac8-4214-a1a2-e8b7d02b18ad"), null, "Delete.User", "Delete.User" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Domain", "ExternalId", "ModifiedAt", "Name", "NormalizedName" },
                values: new object[] { 10, new DateTime(2019, 8, 15, 9, 26, 39, 617, DateTimeKind.Utc).AddTicks(4545), "Ability to navigate to user details page without ability to edit user", 2, new Guid("57c57548-266d-4cd3-a35e-de256c6cedd8"), null, "Read.User", "Read.User" });

            migrationBuilder.InsertData(
                schema: "usm",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 9, 2 },
                    { 10, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "usm",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "usm",
                table: "RolePermission",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "usm",
                table: "Permissions",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "usm",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "ExternalId" },
                values: new object[] { "a597a307a5a04fc39f70917a223b559f", new Guid("8aea15f9-de6b-47a0-9071-2b0c810b280c") });
        }
    }
}
