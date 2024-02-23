using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovescustomtypesfromProjectModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_bug_ProjectModelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_bug_AspNetUsers_AuthorId",
                table: "bug");

            migrationBuilder.DropForeignKey(
                name: "FK_project_bug_ProjectModelId",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_ProjectModelId",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_bug_AuthorId",
                table: "bug");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjectModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "project");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "bug");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectModelId",
                table: "project",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "bug",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectModelId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_ProjectModelId",
                table: "project",
                column: "ProjectModelId");

            migrationBuilder.CreateIndex(
                name: "IX_bug_AuthorId",
                table: "bug",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProjectModelId",
                table: "AspNetUsers",
                column: "ProjectModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_bug_ProjectModelId",
                table: "AspNetUsers",
                column: "ProjectModelId",
                principalTable: "bug",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bug_AspNetUsers_AuthorId",
                table: "bug",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_bug_ProjectModelId",
                table: "project",
                column: "ProjectModelId",
                principalTable: "bug",
                principalColumn: "Id");
        }
    }
}
