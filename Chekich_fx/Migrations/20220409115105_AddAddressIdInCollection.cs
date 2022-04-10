using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chekich_fx.Migrations
{
    public partial class AddAddressIdInCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Address_AtId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Address_ToId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_ToId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Collections_AtId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ToId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "AtId",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_AddressId",
                table: "Deliveries",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_AddressId",
                table: "Collections",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Address_AddressId",
                table: "Collections",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Address_AddressId",
                table: "Deliveries",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Address_AddressId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Address_AddressId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_AddressId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Collections_AddressId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "ToId",
                table: "Deliveries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AtId",
                table: "Collections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ToId",
                table: "Deliveries",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_AtId",
                table: "Collections",
                column: "AtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Address_AtId",
                table: "Collections",
                column: "AtId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Address_ToId",
                table: "Deliveries",
                column: "ToId",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
