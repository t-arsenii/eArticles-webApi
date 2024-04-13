using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eArticles.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class update20hotfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTypes_ContentTypeId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleTypeId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "ContentTypeId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTypes_ContentTypeId",
                table: "Articles",
                column: "ContentTypeId",
                principalTable: "ArticleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleTypes_ContentTypeId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "ContentTypeId",
                table: "Articles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ArticleTypeId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleTypes_ContentTypeId",
                table: "Articles",
                column: "ContentTypeId",
                principalTable: "ArticleTypes",
                principalColumn: "Id");
        }
    }
}
