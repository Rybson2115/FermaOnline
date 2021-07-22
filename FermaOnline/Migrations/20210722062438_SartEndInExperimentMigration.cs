using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class SartEndInExperimentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Experiment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Experiment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Experiment");
        }
    }
}
