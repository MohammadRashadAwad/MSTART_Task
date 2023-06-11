using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSTART_Task.Migrations
{
    /// <inheritdoc />
    public partial class AddTableUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Server_DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTime_UTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_DateTime_UTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Username = table.Column<string>(type: "varchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Date_Of_Birth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
