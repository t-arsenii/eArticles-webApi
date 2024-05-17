using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eArticles.API.Migrations
{
    /// <inheritdoc />
    public partial class bookmarkhotfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_UserId",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "ArticldeId",
                table: "Bookmarks");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId_ArticleId",
                table: "Bookmarks",
                columns: new[] { "UserId", "ArticleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_UserId_ArticleId",
                table: "Bookmarks");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticldeId",
                table: "Bookmarks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId",
                table: "Bookmarks",
                column: "UserId");
        }
    }
}
