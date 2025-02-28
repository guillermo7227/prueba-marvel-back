using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marvel.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteExtraFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextDetail",
                table: "Favorite",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPath",
                table: "Favorite",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlDetailPage",
                table: "Favorite",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextDetail",
                table: "Favorite");

            migrationBuilder.DropColumn(
                name: "ThumbnailPath",
                table: "Favorite");

            migrationBuilder.DropColumn(
                name: "UrlDetailPage",
                table: "Favorite");
        }
    }
}
