using Microsoft.EntityFrameworkCore.Migrations;

namespace AOTA_Server.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_PlayerData_playerDataid",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_playerDataid",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "playerDataid",
                table: "Session");

            migrationBuilder.AddColumn<long>(
                name: "idUser",
                table: "Session",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idUser",
                table: "Session");

            migrationBuilder.AddColumn<long>(
                name: "playerDataid",
                table: "Session",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Session_playerDataid",
                table: "Session",
                column: "playerDataid");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_PlayerData_playerDataid",
                table: "Session",
                column: "playerDataid",
                principalTable: "PlayerData",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
