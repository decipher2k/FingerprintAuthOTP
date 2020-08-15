using Microsoft.EntityFrameworkCore.Migrations;

namespace AOTA_Server.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Building_PlayerData_PlayerDataid",
                table: "Building");

            migrationBuilder.DropIndex(
                name: "IX_Building_PlayerDataid",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "PlayerDataid",
                table: "Building");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PlayerDataid",
                table: "Building",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Building_PlayerDataid",
                table: "Building",
                column: "PlayerDataid");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_PlayerData_PlayerDataid",
                table: "Building",
                column: "PlayerDataid",
                principalTable: "PlayerData",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
