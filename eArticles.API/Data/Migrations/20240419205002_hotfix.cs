using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eArticles.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class hotfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTypes_ContentTypeId",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleTypes",
                table: "ArticleTypes");

            migrationBuilder.RenameTable(
                name: "ArticleTypes",
                newName: "ContentTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleTypes_Title",
                table: "ContentTypes",
                newName: "IX_ContentTypes_Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentTypes",
                table: "ContentTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ContentTypes_ContentTypeId",
                table: "Articles",
                column: "ContentTypeId",
                principalTable: "ContentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ContentTypes_ContentTypeId",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentTypes",
                table: "ContentTypes");

            migrationBuilder.RenameTable(
                name: "ContentTypes",
                newName: "ArticleTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ContentTypes_Title",
                table: "ArticleTypes",
                newName: "IX_ArticleTypes_Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleTypes",
                table: "ArticleTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTypes_ContentTypeId",
                table: "Articles",
                column: "ContentTypeId",
                principalTable: "ArticleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
