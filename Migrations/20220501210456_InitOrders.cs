using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEshop.Migrations
{
    public partial class InitOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatus = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    StateCode = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductsProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductsProductID",
                        column: x => x.ProductsProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductsProductID",
                table: "Orders",
                column: "ProductsProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
