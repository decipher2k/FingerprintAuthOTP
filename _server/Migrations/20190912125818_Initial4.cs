using Microsoft.EntityFrameworkCore.Migrations;

namespace AOTA_Server.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerData_Resources_Resourcesid",
                table: "PlayerData");

            migrationBuilder.DropIndex(
                name: "IX_PlayerData_Resourcesid",
                table: "PlayerData");

            migrationBuilder.DropColumn(
                name: "Gold",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Iron",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Mana",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Resourcesid",
                table: "PlayerData");

            migrationBuilder.RenameColumn(
                name: "Silver",
                table: "Resources",
                newName: "idUser");

            migrationBuilder.AddColumn<long>(
                name: "Gold",
                table: "PlayerData",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Iron",
                table: "PlayerData",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Mana",
                table: "PlayerData",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Silver",
                table: "PlayerData",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gold",
                table: "PlayerData");

            migrationBuilder.DropColumn(
                name: "Iron",
                table: "PlayerData");

            migrationBuilder.DropColumn(
                name: "Mana",
                table: "PlayerData");

            migrationBuilder.DropColumn(
                name: "Silver",
                table: "PlayerData");

            migrationBuilder.RenameColumn(
                name: "idUser",
                table: "Resources",
                newName: "Silver");

            migrationBuilder.AddColumn<long>(
                name: "Gold",
                table: "Resources",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Iron",
                table: "Resources",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Mana",
                table: "Resources",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Resourcesid",
                table: "PlayerData",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerData_Resourcesid",
                table: "PlayerData",
                column: "Resourcesid");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerData_Resources_Resourcesid",
                table: "PlayerData",
                column: "Resourcesid",
                principalTable: "Resources",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
