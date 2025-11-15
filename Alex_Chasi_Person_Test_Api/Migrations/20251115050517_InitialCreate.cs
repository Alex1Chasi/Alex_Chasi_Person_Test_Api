using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alex_Chasi_Person_Test_Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Address", "Age", "City", "Country", "CreatedAt", "Email", "Name", "PhoneNumber", "UpdatedAt" },
                values: new object[] { 1, "Calle Principal 123", 30, "Quito", "Ecuador", new DateTime(2025, 11, 15, 5, 5, 16, 970, DateTimeKind.Utc).AddTicks(4985), "juan.perez@example.com", "Juan Pérez", "+593991234567", null });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Address", "Age", "City", "Country", "CreatedAt", "Email", "Name", "PhoneNumber", "UpdatedAt" },
                values: new object[] { 2, "Av. Amazonas 456", 25, "Guayaquil", "Ecuador", new DateTime(2025, 11, 15, 5, 5, 16, 970, DateTimeKind.Utc).AddTicks(4990), "maria.garcia@example.com", "María García", "+593987654321", null });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Address", "Age", "City", "Country", "CreatedAt", "Email", "Name", "PhoneNumber", "UpdatedAt" },
                values: new object[] { 3, "Calle Sucre 789", 35, "Cuenca", "Ecuador", new DateTime(2025, 11, 15, 5, 5, 16, 970, DateTimeKind.Utc).AddTicks(4993), "carlos.rodriguez@example.com", "Carlos Rodríguez", "+593971112222", null });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Email",
                table: "Persons",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
