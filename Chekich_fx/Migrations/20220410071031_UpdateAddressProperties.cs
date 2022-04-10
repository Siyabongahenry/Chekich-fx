using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chekich_fx.Migrations
{
    public partial class UpdateAddressProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Address",
                newName: "Surbub");

            migrationBuilder.AlterColumn<string>(
                name: "TownOrCity",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "Surbub",
                table: "Address",
                newName: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "TownOrCity",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
