using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Server.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_UserModel_AuthorId",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_project_UserModel_AssignedToId",
                table: "project");

            migrationBuilder.DropForeignKey(
                name: "FK_project_UserModel_ReportedById",
                table: "project");

            migrationBuilder.DropTable(
                name: "UserModel");

            migrationBuilder.DropColumn(
                name: "BugCount",
                table: "bug");

            migrationBuilder.AlterColumn<string>(
                name: "ReportedById",
                table: "project",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "AssignedToId",
                table: "project",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "comment",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ProjectModelId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_bug_ProjectModelId",
                table: "AspNetUsers");

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
                name: "IX_AspNetUsers_ProjectModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReportedById",
                table: "project",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedToId",
                table: "project",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "comment",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BugCount",
                table: "bug",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectModelId = table.Column<int>(type: "INTEGER", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModel_bug_ProjectModelId",
                        column: x => x.ProjectModelId,
                        principalTable: "bug",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_ProjectModelId",
                table: "UserModel",
                column: "ProjectModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_UserModel_AuthorId",
                table: "comment",
                column: "AuthorId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_UserModel_AssignedToId",
                table: "project",
                column: "AssignedToId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_UserModel_ReportedById",
                table: "project",
                column: "ReportedById",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
