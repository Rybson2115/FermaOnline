using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cage",
                columns: table => new
                {
                    CageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CageQuantity = table.Column<int>(type: "int", nullable: false),
                    GroupWeight = table.Column<float>(type: "real", nullable: false),
                    DeathCount = table.Column<int>(type: "int", nullable: false),
                    IndividualBodyWeight = table.Column<float>(type: "real", nullable: false),
                    DifferenceInBodyWeight = table.Column<float>(type: "real", nullable: false),
                    WeightGainFromStart = table.Column<float>(type: "real", nullable: false),
                    WeightGainFromLastSurvey = table.Column<float>(type: "real", nullable: false),
                    AverageWeightGainFromStart = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cage", x => x.CageId);
                });

            migrationBuilder.CreateTable(
                name: "Experiment",
                columns: table => new
                {
                    ExperymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiment", x => x.ExperymentId);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperymentId = table.Column<int>(type: "int", nullable: false),
                    SurveyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfLife = table.Column<int>(type: "int", nullable: false),
                    AverageBodyWeight = table.Column<float>(type: "real", nullable: false),
                    LoculusQuantity = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    DaysFromFirstWeight = table.Column<int>(type: "int", nullable: false),
                    LoculusFeedIntake = table.Column<float>(type: "real", nullable: false),
                    FeedIntakeWeekly = table.Column<float>(type: "real", nullable: false),
                    FeedIntakDaily = table.Column<float>(type: "real", nullable: false),
                    FeedConversionRatio = table.Column<float>(type: "real", nullable: false),
                    AverageWeightGain = table.Column<float>(type: "real", nullable: false),
                    ACageId = table.Column<int>(type: "int", nullable: true),
                    BCageId = table.Column<int>(type: "int", nullable: true),
                    ExperimentExperymentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.SurveyId);
                    table.ForeignKey(
                        name: "FK_Surveys_Cage_ACageId",
                        column: x => x.ACageId,
                        principalTable: "Cage",
                        principalColumn: "CageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Surveys_Cage_BCageId",
                        column: x => x.BCageId,
                        principalTable: "Cage",
                        principalColumn: "CageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Surveys_Experiment_ExperimentExperymentId",
                        column: x => x.ExperimentExperymentId,
                        principalTable: "Experiment",
                        principalColumn: "ExperymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_ACageId",
                table: "Surveys",
                column: "ACageId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_BCageId",
                table: "Surveys",
                column: "BCageId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_ExperimentExperymentId",
                table: "Surveys",
                column: "ExperimentExperymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Cage");

            migrationBuilder.DropTable(
                name: "Experiment");
        }
    }
}
