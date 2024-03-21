using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedApiKeyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "project");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "project");

            migrationBuilder.DropColumn(
                name: "Severity",
                table: "project");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "project");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "bug");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "project",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ReproductionSteps",
                table: "project",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DateResolved",
                table: "project",
                newName: "RepositoryLink");

            migrationBuilder.RenameColumn(
                name: "DateReported",
                table: "project",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "bug",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "RepositoryLink",
                table: "bug",
                newName: "DateResolved");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "bug",
                newName: "ReproductionSteps");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "bug",
                newName: "DateReported");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "project",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "bug",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "bug",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Severity",
                table: "bug",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "bug",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "project");

            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "bug");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "bug");

            migrationBuilder.DropColumn(
                name: "Severity",
                table: "bug");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "bug");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "project",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "RepositoryLink",
                table: "project",
                newName: "DateResolved");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "project",
                newName: "ReproductionSteps");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "project",
                newName: "DateReported");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "bug",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ReproductionSteps",
                table: "bug",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DateResolved",
                table: "bug",
                newName: "RepositoryLink");

            migrationBuilder.RenameColumn(
                name: "DateReported",
                table: "bug",
                newName: "AuthorId");

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "project",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "project",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Severity",
                table: "project",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "project",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "bug",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
