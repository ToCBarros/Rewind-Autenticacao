using Microsoft.EntityFrameworkCore.Migrations;

namespace Rewind.Data.Migrations
{
    public partial class EstudioEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Estudios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "b85b86e6-c80c-41c5-815b-d1fd710a7f09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "ecd5c5d7-273d-4680-b1c9-1c0fc69a5913");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ADMIN",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4796bfc0-5655-4871-a266-3d370aa2c4b0", "AQAAAAEAACcQAAAAEJe658S8iTztaOg5X6JzqfkHrnFRmM5hD27RLcp4G1BkOLiQ889be9VCIT6tONEyHA==", "93b3c37f-06dc-4f7b-a88e-24248648c75e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Estudios");

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
    }
}
