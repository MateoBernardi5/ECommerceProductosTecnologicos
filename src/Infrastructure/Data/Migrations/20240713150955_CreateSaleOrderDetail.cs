using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateSaleOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrdersDetails_Products_ProductId",
                table: "SaleOrdersDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrdersDetails_SaleOrders_SaleOrderId",
                table: "SaleOrdersDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrdersDetails",
                table: "SaleOrdersDetails");

            migrationBuilder.RenameTable(
                name: "SaleOrdersDetails",
                newName: "SaleOrderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrdersDetails_SaleOrderId",
                table: "SaleOrderDetails",
                newName: "IX_SaleOrderDetails_SaleOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrdersDetails_ProductId",
                table: "SaleOrderDetails",
                newName: "IX_SaleOrderDetails_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrderDetails",
                table: "SaleOrderDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetails_Products_ProductId",
                table: "SaleOrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                table: "SaleOrderDetails",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetails_Products_ProductId",
                table: "SaleOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                table: "SaleOrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrderDetails",
                table: "SaleOrderDetails");

            migrationBuilder.RenameTable(
                name: "SaleOrderDetails",
                newName: "SaleOrdersDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrderDetails_SaleOrderId",
                table: "SaleOrdersDetails",
                newName: "IX_SaleOrdersDetails_SaleOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrdersDetails",
                newName: "IX_SaleOrdersDetails_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrdersDetails",
                table: "SaleOrdersDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrdersDetails_Products_ProductId",
                table: "SaleOrdersDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrdersDetails_SaleOrders_SaleOrderId",
                table: "SaleOrdersDetails",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
