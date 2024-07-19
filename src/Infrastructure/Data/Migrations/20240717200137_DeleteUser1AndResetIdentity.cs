using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUser1AndResetIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminar el usuario con Id 1
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            // Eliminar el usuario con Id 100
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 100);

            // Reiniciar la secuencia de identidad para SQLite
            migrationBuilder.Sql("UPDATE sqlite_sequence SET seq = (SELECT MAX(Id) FROM Users) WHERE name = 'Users';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Volver a insertar el usuario con Id 1
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "LastName", "Name", "Password", "UserName", "UserType" },
                values: new object[] { 1, "string", "string", "string", "string", "string", "string", "string" });

            // Volver a insertar el usuario con Id 100
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "UserName", "UserType" },
                values: new object[] { 100, "juanperez@gmail.com", "Perez", "Juan", "perez123", "juan", "Admin" });

            // Reiniciar la secuencia de identidad para SQLite
            migrationBuilder.Sql("UPDATE sqlite_sequence SET seq = (SELECT MAX(Id) FROM Users) WHERE name = 'Users';");
        }
    }
}
