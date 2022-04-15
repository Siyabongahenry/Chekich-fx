using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chekich_fx.Migrations
{
    public partial class UpdateOrderProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OnlinePayments_OrderId",
                table: "OnlinePayments");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_OrderId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Collections_OrderId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_CashPayments_OrderId",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ReceivalType",
                table: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_OnlinePayments_OrderId",
                table: "OnlinePayments",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_OrderId",
                table: "Deliveries",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_OrderId",
                table: "Collections",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_OrderId",
                table: "CashPayments",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OnlinePayments_OrderId",
                table: "OnlinePayments");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_OrderId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Collections_OrderId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_CashPayments_OrderId",
                table: "CashPayments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceivalType",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OnlinePayments_OrderId",
                table: "OnlinePayments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_OrderId",
                table: "Deliveries",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_OrderId",
                table: "Collections",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_OrderId",
                table: "CashPayments",
                column: "OrderId");
        }
    }
}
