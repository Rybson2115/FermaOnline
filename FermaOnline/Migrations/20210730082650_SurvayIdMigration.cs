using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class SurvayIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CageAId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "CageBId",
                table: "Surveys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CageAId",
                table: "Surveys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CageBId",
                table: "Surveys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
