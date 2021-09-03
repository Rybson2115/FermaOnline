using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class NewNameInSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AverageWeightGain",
                table: "Surveys",
                newName: "AverageWeightGainFromCages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AverageWeightGainFromCages",
                table: "Surveys",
                newName: "AverageWeightGain");
        }
    }
}
