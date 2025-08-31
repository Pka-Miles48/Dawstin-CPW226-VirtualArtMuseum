using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dawstin_CPW226_VirtualArtMuseum.Migrations
{
    /// <inheritdoc />
    public partial class AddBioToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeedbackNote",
                table: "Artworks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackNote",
                table: "Artworks");
        }
    }
}
