using Microsoft.EntityFrameworkCore.Migrations;

namespace eSportTournament.Migrations
{
    public partial class addNbJoeurs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nombreJoueurs",
                table: "Competitions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nombreJoueurs",
                table: "Competitions");
        }
    }
}
