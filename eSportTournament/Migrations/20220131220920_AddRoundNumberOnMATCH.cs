using Microsoft.EntityFrameworkCore.Migrations;

namespace eSportTournament.Migrations
{
    public partial class AddRoundNumberOnMATCH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "roundNumber",
                table: "Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "roundNumber",
                table: "Matchs");
        }
    }
}
