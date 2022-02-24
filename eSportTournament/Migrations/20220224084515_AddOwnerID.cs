using Microsoft.EntityFrameworkCore.Migrations;

namespace eSportTournament.Migrations
{
    public partial class AddOwnerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ownerID",
                table: "Equipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ownerID",
                table: "Competitions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ownerID",
                table: "Equipes");

            migrationBuilder.DropColumn(
                name: "ownerID",
                table: "Competitions");
        }
    }
}
