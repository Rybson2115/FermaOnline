using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class ChangePropNameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Experiment_ExperimentExperymentId",
                table: "Surveys");

            migrationBuilder.RenameColumn(
                name: "ExperimentExperymentId",
                table: "Surveys",
                newName: "ExperimentId");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_ExperimentExperymentId",
                table: "Surveys",
                newName: "IX_Surveys_ExperimentId");

            migrationBuilder.RenameColumn(
                name: "ExperymentId",
                table: "Experiment",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Experiment_ExperimentId",
                table: "Surveys",
                column: "ExperimentId",
                principalTable: "Experiment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Experiment_ExperimentId",
                table: "Surveys");

            migrationBuilder.RenameColumn(
                name: "ExperimentId",
                table: "Surveys",
                newName: "ExperimentExperymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_ExperimentId",
                table: "Surveys",
                newName: "IX_Surveys_ExperimentExperymentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Experiment",
                newName: "ExperymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Experiment_ExperimentExperymentId",
                table: "Surveys",
                column: "ExperimentExperymentId",
                principalTable: "Experiment",
                principalColumn: "ExperymentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
