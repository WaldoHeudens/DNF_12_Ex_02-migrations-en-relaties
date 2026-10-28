using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _02_migrations_en_relaties.Migrations
{
    /// <inheritdoc />
    public partial class BeginModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naam = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boeken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titel = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Jaar = table.Column<int>(type: "INTEGER", nullable: false),
                    AuteurId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boeken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boeken_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Auteurs",
                columns: new[] { "Id", "Naam" },
                values: new object[,]
                {
                    { 1, "Robert C. Martin" },
                    { 2, "Martin Fowler" }
                });

            migrationBuilder.InsertData(
                table: "Boeken",
                columns: new[] { "Id", "AuteurId", "Jaar", "Titel" },
                values: new object[,]
                {
                    { 1, 1, 2008, "Clean Code" },
                    { 2, 1, 2017, "Clean Architecture" },
                    { 3, 2, 1999, "Refactoring" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boeken_AuteurId",
                table: "Boeken",
                column: "AuteurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boeken");

            migrationBuilder.DropTable(
                name: "Auteurs");
        }
    }
}
