using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eArticles.API.Migrations
{
    /// <inheritdoc />
    public partial class imagepatharticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img_Url",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Img_Url",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
