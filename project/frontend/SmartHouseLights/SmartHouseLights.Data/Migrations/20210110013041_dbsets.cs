using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHouseLights.Data.Migrations
{
    public partial class dbsets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLightStatistics_Light_LightId",
                table: "UserLightStatistics");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLightStatistics_User_UserId",
                table: "UserLightStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Light",
                table: "Light");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Light",
                newName: "Lights");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lights",
                table: "Lights",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLightStatistics_Lights_LightId",
                table: "UserLightStatistics",
                column: "LightId",
                principalTable: "Lights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLightStatistics_Users_UserId",
                table: "UserLightStatistics",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLightStatistics_Lights_LightId",
                table: "UserLightStatistics");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLightStatistics_Users_UserId",
                table: "UserLightStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lights",
                table: "Lights");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Lights",
                newName: "Light");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Light",
                table: "Light",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLightStatistics_Light_LightId",
                table: "UserLightStatistics",
                column: "LightId",
                principalTable: "Light",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLightStatistics_User_UserId",
                table: "UserLightStatistics",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
