using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthorandAuthorIdtoProjectModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "bug",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_bug_AuthorId",
                table: "bug",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_bug_AspNetUsers_AuthorId",
                table: "bug",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bug_AspNetUsers_AuthorId",
                table: "bug");

            migrationBuilder.DropIndex(
                name: "IX_bug_AuthorId",
                table: "bug");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "bug");
        }
    }
}
