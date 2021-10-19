using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class FileMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Experiment_ExperimentId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_ExperimentId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "Extension",
                table: "Files",
                newName: "FileType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "Files",
                newName: "Extension");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ExperimentId",
                table: "Files",
                column: "ExperimentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Experiment_ExperimentId",
                table: "Files",
                column: "ExperimentId",
                principalTable: "Experiment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
