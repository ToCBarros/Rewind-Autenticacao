using Microsoft.EntityFrameworkCore.Migrations;

namespace Rewind.Data.Migrations
{
    public partial class SeriesAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "c30a6636-e32c-4094-b695-75d9da14d90a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "20c1bbc9-cfc6-45d8-89d0-2e9550e3e124");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ADMIN",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f8451f9-0dfb-48c2-a80b-e134a2b3fa7e", "AQAAAAEAACcQAAAAEKLojmAhwfMRkojhoH8XArukHhxmm2BCvc9LCBggOMj6GaWu1DDu6SVTLaAskCDC/A==", "7d52e6a7-bba9-40bc-8bb1-9e4b66268a1a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                column: "ConcurrencyStamp",
                value: "2b73b980-f0cc-4f5f-ac14-c0084a3edfd4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ADMIN",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f35af4e0-8157-4b8f-b806-d9a85d38d15b", "AQAAAAEAACcQAAAAEBIEthzu0GG3PatHhZ4tw+ELX5FQqmGyVS1JSS8IQyknb3OsoCZlzk9QhlgOQZljOw==", "830f5d23-ba95-448d-98c9-c5fe79f4fcb0" });
        }
    }
}
