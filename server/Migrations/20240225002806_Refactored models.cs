using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Server.Migrations
{
    /// <inheritdoc />
    public partial class Refactoredmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_AspNetUsers_AuthorId",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_project_AspNetUsers_AssignedToId",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_project_AspNetUsers_ReportedById",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_AssignedToId",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_ReportedById",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_comment_AuthorId",
                table: "comment");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "project");

            migrationBuilder.DropColumn(
                name: "ReportedById",
                table: "project");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "comment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedToId",
                table: "project",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportedById",
                table: "project",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "comment",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_AssignedToId",
                table: "project",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_project_ReportedById",
                table: "project",
                column: "ReportedById");

            migrationBuilder.CreateIndex(
                name: "IX_comment_AuthorId",
                table: "comment",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_AspNetUsers_AuthorId",
                table: "comment",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_AspNetUsers_AssignedToId",
                table: "project",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_AspNetUsers_ReportedById",
                table: "project",
                column: "ReportedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
