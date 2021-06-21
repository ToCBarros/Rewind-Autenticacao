using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rewind.Data.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estudio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Utilizador = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinopse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Episodios = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstudioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Series_Estudios_EstudioID",
                        column: x => x.EstudioID,
                        principalTable: "Estudios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadoresID = table.Column<int>(type: "int", nullable: false),
                    SeriesID = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estrelas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comentarios_Series_SeriesID",
                        column: x => x.SeriesID,
                        principalTable: "Series",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Utilizadores_UtilizadoresID",
                        column: x => x.UtilizadoresID,
                        principalTable: "Utilizadores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Estudios",
                columns: new[] { "ID", "Estudio", "Pais" },
                values: new object[,]
                {
                    { 1, "ABC", "Portugal" },
                    { 2, "ACR", "França" },
                    { 3, "TCB", "Espanha" }
                });

            migrationBuilder.InsertData(
                table: "Utilizadores",
                columns: new[] { "ID", "Email", "UserName", "Utilizador" },
                values: new object[,]
                {
                    { 1, "a@aa", null, "admin" },
                    { 2, "b@bb", null, "antonio" },
                    { 3, "c@cc", null, "tomas" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "ID", "Ano", "Data", "Episodios", "Estado", "EstudioID", "Imagem", "Sinopse", "Titulo" },
                values: new object[] { 1, 2004, new DateTime(2021, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "continuando", 1, "a.jpg", "Morbi laoreet neque", "Lorem ipsum" });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "ID", "Ano", "Data", "Episodios", "Estado", "EstudioID", "Imagem", "Sinopse", "Titulo" },
                values: new object[] { 2, 2005, new DateTime(2021, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "terminado", 2, "b.jpg", "ut erat gravida", "dolor sit amet" });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "ID", "Ano", "Data", "Episodios", "Estado", "EstudioID", "Imagem", "Sinopse", "Titulo" },
                values: new object[] { 3, 2012, new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "continuando", 3, "c.jpg", "Integer mattis lorem et lorem", "consectetur adipiscing elit" });

            migrationBuilder.InsertData(
                table: "Comentarios",
                columns: new[] { "ID", "Comentario", "Data", "Estado", "Estrelas", "SeriesID", "UtilizadoresID" },
                values: new object[] { 1, "bom", new DateTime(2021, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "visivel", 5, 1, 1 });

            migrationBuilder.InsertData(
                table: "Comentarios",
                columns: new[] { "ID", "Comentario", "Data", "Estado", "Estrelas", "SeriesID", "UtilizadoresID" },
                values: new object[] { 2, "mau", new DateTime(2021, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "invisivel", 4, 2, 2 });

            migrationBuilder.InsertData(
                table: "Comentarios",
                columns: new[] { "ID", "Comentario", "Data", "Estado", "Estrelas", "SeriesID", "UtilizadoresID" },
                values: new object[] { 3, "mais ou menos", new DateTime(2020, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "visivel", 3, 3, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_SeriesID",
                table: "Comentarios",
                column: "SeriesID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UtilizadoresID",
                table: "Comentarios",
                column: "UtilizadoresID");

            migrationBuilder.CreateIndex(
                name: "IX_Series_EstudioID",
                table: "Series",
                column: "EstudioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Estudios");
        }
    }
}
