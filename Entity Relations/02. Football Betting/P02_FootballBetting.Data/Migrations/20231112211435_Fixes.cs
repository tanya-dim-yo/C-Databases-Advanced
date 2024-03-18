using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P02_FootballBetting.Data.Migrations
{
    public partial class Fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePlayer",
                columns: table => new
                {
                    PlayersStatisticsGameId = table.Column<int>(type: "int", nullable: false),
                    PlayersStatisticsPlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayer", x => new { x.PlayersStatisticsGameId, x.PlayersStatisticsPlayerId });
                    table.ForeignKey(
                        name: "FK_GamePlayer_Games_PlayersStatisticsGameId",
                        column: x => x.PlayersStatisticsGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayer_Players_PlayersStatisticsPlayerId",
                        column: x => x.PlayersStatisticsPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayer_PlayersStatisticsPlayerId",
                table: "GamePlayer",
                column: "PlayersStatisticsPlayerId");
        }
    }
}
