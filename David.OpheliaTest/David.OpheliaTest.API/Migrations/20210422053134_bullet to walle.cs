using Microsoft.EntityFrameworkCore.Migrations;

namespace David.OpheliaTest.API.Migrations
{
    public partial class bullettowalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bullets_Users_UserId",
                table: "Bullets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bullets",
                table: "Bullets");

            migrationBuilder.RenameTable(
                name: "Bullets",
                newName: "Wallets");

            migrationBuilder.RenameIndex(
                name: "IX_Bullets_UserId",
                table: "Wallets",
                newName: "IX_Wallets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "Bullets");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_UserId",
                table: "Bullets",
                newName: "IX_Bullets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bullets",
                table: "Bullets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bullets_Users_UserId",
                table: "Bullets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
