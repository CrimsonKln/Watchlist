using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Watchlist.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationFilmUtilisateur2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmsUtilisateur_AspNetUsers_UserId",
                table: "FilmsUtilisateur");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmsUtilisateur",
                table: "FilmsUtilisateur");

            migrationBuilder.DropIndex(
                name: "IX_FilmsUtilisateur_UserId",
                table: "FilmsUtilisateur");

            migrationBuilder.DropColumn(
                name: "IdUtilisateur",
                table: "FilmsUtilisateur");

            migrationBuilder.DropColumn(
                name: "IdFilm",
                table: "FilmsUtilisateur");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FilmsUtilisateur",
                newName: "UtilisateurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmsUtilisateur",
                table: "FilmsUtilisateur",
                columns: new[] { "UtilisateurId", "FilmId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FilmsUtilisateur_AspNetUsers_UtilisateurId",
                table: "FilmsUtilisateur",
                column: "UtilisateurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmsUtilisateur_AspNetUsers_UtilisateurId",
                table: "FilmsUtilisateur");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmsUtilisateur",
                table: "FilmsUtilisateur");

            migrationBuilder.RenameColumn(
                name: "UtilisateurId",
                table: "FilmsUtilisateur",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "IdUtilisateur",
                table: "FilmsUtilisateur",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdFilm",
                table: "FilmsUtilisateur",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmsUtilisateur",
                table: "FilmsUtilisateur",
                columns: new[] { "IdUtilisateur", "IdFilm" });

            migrationBuilder.CreateIndex(
                name: "IX_FilmsUtilisateur_UserId",
                table: "FilmsUtilisateur",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmsUtilisateur_AspNetUsers_UserId",
                table: "FilmsUtilisateur",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
