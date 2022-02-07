using Microsoft.EntityFrameworkCore.Migrations;

namespace eSportTournament.Migrations
{
    public partial class AddDemandeEquipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DemandesEquipes",
                table: "DemandesEquipes");

            migrationBuilder.RenameTable(
                name: "DemandesEquipes",
                newName: "DemandeEquipe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DemandeEquipe",
                table: "DemandeEquipe",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DemandeEquipe",
                table: "DemandeEquipe");

            migrationBuilder.RenameTable(
                name: "DemandeEquipe",
                newName: "DemandesEquipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DemandesEquipes",
                table: "DemandesEquipes",
                column: "ID");
        }
    }
}
