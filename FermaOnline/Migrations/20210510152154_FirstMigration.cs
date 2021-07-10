using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cage",
                columns: table => new
                {
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CageQuantity = table.Column<int>(type: "int", nullable: false),
                    GroupWeight = table.Column<float>(type: "real", nullable: false),
                    DeathCount = table.Column<int>(type: "int", nullable: false),
                    IndividualBodyWeight = table.Column<float>(type: "real", nullable: false),
                    AverageBodyWeight = table.Column<float>(type: "real", nullable: false),
                    DifferenceInBodyWeight = table.Column<float>(type: "real", nullable: false),
                    WeightGainFromStart = table.Column<float>(type: "real", nullable: false),
                    WeightGainFromLastSurvey = table.Column<float>(type: "real", nullable: false),
                    AverageWeightGain = table.Column<float>(type: "real", nullable: false),
                    AverageWeightGainFromStart = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cage", x => x.SurveyId);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    SurveyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfLife = table.Column<int>(type: "int", nullable: false),
                    LoculusQuantity = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    DaysFromFirstWeight = table.Column<int>(type: "int", nullable: false),
                    LoculusFeedIntake = table.Column<float>(type: "real", nullable: false),
                    FeedIntakeWeekly = table.Column<float>(type: "real", nullable: false),
                    FeedIntakDaily = table.Column<float>(type: "real", nullable: false),
                    FeedConversionRatio = table.Column<float>(type: "real", nullable: false),
                    ASurveyId = table.Column<int>(type: "int", nullable: true),
                    BSurveyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.SurveyId);
                    table.ForeignKey(
                        name: "FK_Surveys_Cage_ASurveyId",
                        column: x => x.ASurveyId,
                        principalTable: "Cage",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Surveys_Cage_BSurveyId",
                        column: x => x.BSurveyId,
                        principalTable: "Cage",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemplateCurve",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<float>(type: "real", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateCurve", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateCurve_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_ASurveyId",
                table: "Surveys",
                column: "ASurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_BSurveyId",
                table: "Surveys",
                column: "BSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateCurve_SurveyId",
                table: "TemplateCurve",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateCurve");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Cage");
        }
    }
}
