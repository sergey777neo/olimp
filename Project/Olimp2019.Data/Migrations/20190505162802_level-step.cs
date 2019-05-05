using Microsoft.EntityFrameworkCore.Migrations;

namespace Olimp2019.Data.Migrations
{
    public partial class levelstep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "NextLevel",
                table: "AspNetUsers",
                newName: "CurrentStep");

            migrationBuilder.AddColumn<int>(
                name: "StepCount",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLevel",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepCount",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "CurrentLevel",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CurrentStep",
                table: "AspNetUsers",
                newName: "NextLevel");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Levels",
                nullable: true);
        }
    }
}
