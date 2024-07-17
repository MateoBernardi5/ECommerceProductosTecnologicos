using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthenticateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Stock" },
                values: new object[] { 1, "Televisor", 540000m, 10 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "LastName", "Name", "Password", "UserName", "UserType" },
                values: new object[] { 2, "Espora 1389", "pazblanco01@gmail.com", "Blanco", "Maria Paz", "paz123", "paz", "Client" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "UserName", "UserType" },
                values: new object[] { 3, "mateobernardi@gmail.com", "Bernardi", "Mateo", "123", "mateo", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "UserName", "UserType" },
                values: new object[,]
                {
                    { 5, "mateobernardi@gmail.com", "Bernardi", "Mateo", "123", "mateo", "Admin" },
                    { 100, "juanperez@gmail.com", "Perez", "Juan", "perez123", "juan", "Admin" }
                });
        }
    }
}
