using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eArticles.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class newTableArticleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Article_type",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "ArticleTypeId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleTypeId",
                table: "Articles",
                column: "ArticleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeId",
                table: "Articles",
                column: "ArticleTypeId",
                principalTable: "ArticleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleTypes");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleTypeId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleTypeId",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Article_type",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
