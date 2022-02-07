using Microsoft.EntityFrameworkCore.Migrations;

namespace eSportTournament.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetitionEquipe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    competitionID = table.Column<int>(type: "int", nullable: false),
                    equipeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionEquipe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompetitionEquipe_Competitions_competitionID",
                        column: x => x.competitionID,
                        principalTable: "Competitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionEquipe_Equipes_equipeID",
                        column: x => x.equipeID,
                        principalTable: "Equipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEquipe_competitionID",
                table: "CompetitionEquipe",
                column: "competitionID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEquipe_equipeID",
                table: "CompetitionEquipe",
                column: "equipeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionEquipe");
        }
    }
}
