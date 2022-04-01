using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chekich_fx.Migrations
{
    public partial class UpdateOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransportPrice",
                table: "Order",
                newName: "ReceivalCost");

            migrationBuilder.AddColumn<int>(
                name: "ReceivalType",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivalType",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ReceivalCost",
                table: "Order",
                newName: "TransportPrice");
        }
    }
}
