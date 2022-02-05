using Microsoft.EntityFrameworkCore.Migrations;

namespace eSportTournament.Migrations
{
    public partial class ouiouiou : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "DemandesEquipeCreations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    equipeID = table.Column<int>(type: "int", nullable: false),
                    approuver = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandesEquipeCreations", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandesEquipeCreations");

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
    }
}
