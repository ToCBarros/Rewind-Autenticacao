using Microsoft.EntityFrameworkCore.Migrations;

namespace Rewind.Data.Migrations
{
    public partial class SEEDADMIN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "5b6033de-fb0f-4517-bb62-c5251d757bbd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "2b73b980-f0cc-4f5f-ac14-c0084a3edfd4", "Utilizador" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ADMIN", 0, "f35af4e0-8157-4b8f-b806-d9a85d38d15b", "a@aa.com", true, false, null, "A@AA.com", "A@AA.com", "AQAAAAEAACcQAAAAEBIEthzu0GG3PatHhZ4tw+ELX5FQqmGyVS1JSS8IQyknb3OsoCZlzk9QhlgOQZljOw==", null, false, "830f5d23-ba95-448d-98c9-c5fe79f4fcb0", false, "a@aa.com" });

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Email", "UserName" },
                values: new object[] { "a@aa.com", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Email", "UserName" },
                values: new object[] { "b@bb.com", "UTILIZADOR" });

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "ID",
                keyValue: 3,
                column: "Email",
                value: "c@cc.com");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "g", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "g", "ADMIN" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "331182f1-835f-4eba-943b-4f79ff0d8fe5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "f3a9b26c-dd3b-4c1b-b0e7-46fce5e4626e", "utilizador" });

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Email", "UserName" },
                values: new object[] { "a@aa", null });

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Email", "UserName" },
                values: new object[] { "b@bb", null });

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "ID",
                keyValue: 3,
                column: "Email",
                value: "c@cc");
        }
    }
}
