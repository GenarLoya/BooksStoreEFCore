using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Crear tabla Authors
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            // 2. Sembrar Authors con los nombres distintos de Books.Author
            migrationBuilder.Sql(
                "INSERT INTO Authors (Name) SELECT DISTINCT Author FROM Books WHERE Author IS NOT NULL;");

            // 3. Añadir AuthorId como nullable (para no fallar con filas existentes)
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "INTEGER",
                nullable: true);

            // 4. Backfill: asignar AuthorId según el nombre del autor
            migrationBuilder.Sql(
                "UPDATE Books SET AuthorId = (SELECT Id FROM Authors WHERE Authors.Name = Books.Author);");

            // 5. Hacer AuthorId NOT NULL
            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            // 6. Eliminar la columna vieja Author
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            // 7. Crear índice para la FK
            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            // 8. Añadir FK
            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            // Repoblar la columna Author desde la tabla Authors antes de borrarla
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "TEXT",
                nullable: true);

            migrationBuilder.Sql(
                "UPDATE Books SET Author = (SELECT Name FROM Authors WHERE Authors.Id = Books.AuthorId);");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
