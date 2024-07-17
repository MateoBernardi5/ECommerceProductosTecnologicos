using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetails_Products_ProductId",
                table: "SaleOrderDetails");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "UserName", "UserType" },
                values: new object[] { 5, "mateobernardi@gmail.com", "Bernardi", "Mateo", "123", "mateo", "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetails_Products_ProductId",
                table: "SaleOrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetails_Products_ProductId",
                table: "SaleOrderDetails");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetails_Products_ProductId",
                table: "SaleOrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
