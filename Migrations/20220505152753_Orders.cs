using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEshop.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductsProductID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductsProductID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductsProductID",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrdersOrderID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrdersOrderID",
                table: "Products",
                column: "OrdersOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrdersOrderID",
                table: "Products",
                column: "OrdersOrderID",
                principalTable: "Orders",
                principalColumn: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrdersOrderID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrdersOrderID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrdersOrderID",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductID",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductsProductID",
                table: "Orders",
                column: "ProductsProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductsProductID",
                table: "Orders",
                column: "ProductsProductID",
                principalTable: "Products",
                principalColumn: "ProductID");
        }
    }
}
