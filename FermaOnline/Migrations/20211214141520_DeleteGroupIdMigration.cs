using Microsoft.EntityFrameworkCore.Migrations;

namespace FermaOnline.Migrations
{
    public partial class DeleteGroupIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Surveys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Surveys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
