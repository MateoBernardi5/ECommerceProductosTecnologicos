using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SaleOrderDetailMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetail_Products_ProductId",
                table: "SaleOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetail_SaleOrders_SaleOrderId",
                table: "SaleOrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrderDetail",
                table: "SaleOrderDetail");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "SaleOrderDetail");

            migrationBuilder.RenameTable(
                name: "SaleOrderDetail",
                newName: "SaleOrdersDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrderDetail_SaleOrderId",
                table: "SaleOrdersDetails",
                newName: "IX_SaleOrdersDetails_SaleOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrderDetail_ProductId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "SaleOrderDetail");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrdersDetails_SaleOrderId",
                table: "SaleOrderDetail",
                newName: "IX_SaleOrderDetail_SaleOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrdersDetails_ProductId",
                table: "SaleOrderDetail",
                newName: "IX_SaleOrderDetail_ProductId");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "SaleOrderDetail",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrderDetail",
                table: "SaleOrderDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetail_Products_ProductId",
                table: "SaleOrderDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetail_SaleOrders_SaleOrderId",
                table: "SaleOrderDetail",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
