using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiper.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<byte>(type: "TINYINT", nullable: false),
                    Birthday = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    Document = table.Column<string>(type: "VARCHAR(18)", maxLength: 18, nullable: false),
                    Document_Type = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    Street = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    Number = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Neighborhood = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    City = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    State = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
