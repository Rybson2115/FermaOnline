using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class MoreCageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cage_Surveys_SurveyId",
                table: "Cage");

            migrationBuilder.DropTable(
                name: "CagesIndex");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "Cage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cage_Surveys_SurveyId",
                table: "Cage",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "SurveyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cage_Surveys_SurveyId",
                table: "Cage");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "Cage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "IX_CagesIndex_SurveyId",
                table: "CagesIndex",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cage_Surveys_SurveyId",
                table: "Cage",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "SurveyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
