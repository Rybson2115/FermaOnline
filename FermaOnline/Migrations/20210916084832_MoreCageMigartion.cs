using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class MoreCageMigartion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Experiment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CageNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperimentId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperimentId = table.Column<int>(type: "int", nullable: false),
                    SurveyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfLife = table.Column<int>(type: "int", nullable: false),
                    AverageBodyWeight = table.Column<float>(type: "real", nullable: false),
                    LoculusQuantity = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    DaysFromFirstWeight = table.Column<int>(type: "int", nullable: false),
                    LoculusFeedInTake = table.Column<float>(type: "real", nullable: false),
                    FeedIntakeWeekly = table.Column<float>(type: "real", nullable: false),
                    FeedIntakDaily = table.Column<float>(type: "real", nullable: false),
                    FeedConversionRatio = table.Column<float>(type: "real", nullable: false),
                    AverageWeightGainFromCages = table.Column<float>(type: "real", nullable: false),
                    AverageWeightGainFromLastSurvey = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.SurveyId);
                    table.ForeignKey(
                        name: "FK_Surveys_Experiment_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    SurveyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cage", x => x.CageId);
                    table.ForeignKey(
                        name: "FK_Cage_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CagesIndex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CageId = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CagesIndex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CagesIndex_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cage_SurveyId",
                table: "Cage",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_CagesIndex_SurveyId",
                table: "CagesIndex",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ExperimentId",
                table: "Image",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_ExperimentId",
                table: "Surveys",
                column: "ExperimentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cage");

            migrationBuilder.DropTable(
                name: "CagesIndex");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Experiment");
        }
    }
}
