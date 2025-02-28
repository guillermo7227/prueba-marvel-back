using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marvel.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteCompositeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorite_ApplicationUserId",
                table: "Favorite");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_ApplicationUserId_ComicId",
                table: "Favorite",
                columns: new[] { "ApplicationUserId", "ComicId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favorite_ApplicationUserId_ComicId",
                table: "Favorite");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_ApplicationUserId",
                table: "Favorite",
                column: "ApplicationUserId");
        }
    }
}
