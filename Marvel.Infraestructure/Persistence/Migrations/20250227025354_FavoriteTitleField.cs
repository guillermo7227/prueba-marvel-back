using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marvel.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteTitleField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Favorite",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Favorite");
        }
    }
}
