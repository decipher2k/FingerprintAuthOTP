using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AOTA_Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingType",
                columns: table => new
                {
                    PrefabID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuildDuration = table.Column<long>(nullable: false),
                    BuildDurationMultipliere = table.Column<long>(nullable: false),
                    BuildCostIron = table.Column<long>(nullable: false),
                    BuildCostGold = table.Column<long>(nullable: false),
                    BuildCostSilver = table.Column<long>(nullable: false),
                    BuildCostMultiplier = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingType", x => x.PrefabID);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Silver = table.Column<long>(nullable: false),
                    Gold = table.Column<long>(nullable: false),
                    Iron = table.Column<long>(nullable: false),
                    Mana = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerData",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Resourcesid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerData", x => x.id);
                    table.ForeignKey(
                        name: "FK_PlayerData_Resources_Resourcesid",
                        column: x => x.Resourcesid,
                        principalTable: "Resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PrefabID = table.Column<long>(nullable: false),
                    isFinished = table.Column<bool>(nullable: false),
                    buildingStartedTimestamp = table.Column<long>(nullable: false),
                    idUser = table.Column<long>(nullable: false),
                    x = table.Column<float>(nullable: false),
                    y = table.Column<float>(nullable: false),
                    z = table.Column<float>(nullable: false),
                    PlayerDataid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.id);
                    table.ForeignKey(
                        name: "FK_Building_PlayerData_PlayerDataid",
                        column: x => x.PlayerDataid,
                        principalTable: "PlayerData",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sessionKey = table.Column<string>(nullable: true),
                    lastUpdate = table.Column<long>(nullable: false),
                    playerDataid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.id);
                    table.ForeignKey(
                        name: "FK_Session_PlayerData_playerDataid",
                        column: x => x.playerDataid,
                        principalTable: "PlayerData",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Building_PlayerDataid",
                table: "Building",
                column: "PlayerDataid");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerData_Resourcesid",
                table: "PlayerData",
                column: "Resourcesid");

            migrationBuilder.CreateIndex(
                name: "IX_Session_playerDataid",
                table: "Session",
                column: "playerDataid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "BuildingType");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "PlayerData");

            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}
