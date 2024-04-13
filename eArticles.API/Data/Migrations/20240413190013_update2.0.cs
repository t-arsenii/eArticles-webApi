using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eArticles.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class update20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleTypeId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ArticleTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ContentTypeId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTypes_Title",
                table: "ArticleTypes",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ContentTypeId",
                table: "Articles",
                column: "ContentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTypes_ContentTypeId",
                table: "Articles",
                column: "ContentTypeId",
                principalTable: "ArticleTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTypes_ContentTypeId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Title",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTypes_Title",
                table: "ArticleTypes");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ContentTypeId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ContentTypeId",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ArticleTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleTypeId",
                table: "Articles",
                column: "ArticleTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTypes_ArticleTypeId",
                table: "Articles",
                column: "ArticleTypeId",
                principalTable: "ArticleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
