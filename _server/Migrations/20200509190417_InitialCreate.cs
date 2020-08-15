using Microsoft.EntityFrameworkCore.Migrations;

namespace AOTA_Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "PlayerData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Session",
                table: "PlayerData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "PlayerData");

            migrationBuilder.DropColumn(
                name: "Session",
                table: "PlayerData");
        }
    }
}
