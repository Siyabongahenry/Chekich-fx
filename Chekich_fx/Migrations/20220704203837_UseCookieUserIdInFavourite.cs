using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chekich_fx.Migrations
{
    public partial class UseCookieUserIdInFavourite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favourites",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favourites",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId",
                table: "Favourites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
