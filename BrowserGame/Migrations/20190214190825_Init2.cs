using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BrowserGame.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExceptionDetails");

            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Persons",
                newName: "Speed");

            migrationBuilder.RenameColumn(
                name: "Health",
                table: "Persons",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "Defense",
                table: "Persons",
                newName: "MaxPoints");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Persons",
                newName: "Attempts");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Speed",
                table: "Persons",
                newName: "Strength");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Persons",
                newName: "Health");

            migrationBuilder.RenameColumn(
                name: "MaxPoints",
                table: "Persons",
                newName: "Defense");

            migrationBuilder.RenameColumn(
                name: "Attempts",
                table: "Persons",
                newName: "Age");

            migrationBuilder.CreateTable(
                name: "ExceptionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActionName = table.Column<string>(nullable: true),
                    ControllerName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ExceptionMessage = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionDetails", x => x.Id);
                });
        }
    }
}
