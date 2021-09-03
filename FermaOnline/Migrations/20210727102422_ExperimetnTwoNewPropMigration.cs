using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class ExperimetnTwoNewPropMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageWeightGainFromStart",
                table: "Cage");

            migrationBuilder.RenameColumn(
                name: "LoculusFeedIntake",
                table: "Surveys",
                newName: "LoculusFeedInTake");

            migrationBuilder.AddColumn<float>(
                name: "AverageWeightGainFromStart",
                table: "Surveys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AFirstIndividualBodyWeight",
                table: "Experiment",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "BFirstIndividualBodyWeight",
                table: "Experiment",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageWeightGainFromStart",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "AFirstIndividualBodyWeight",
                table: "Experiment");

            migrationBuilder.DropColumn(
                name: "BFirstIndividualBodyWeight",
                table: "Experiment");

            migrationBuilder.RenameColumn(
                name: "LoculusFeedInTake",
                table: "Surveys",
                newName: "LoculusFeedIntake");

            migrationBuilder.AddColumn<float>(
                name: "AverageWeightGainFromStart",
                table: "Cage",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
