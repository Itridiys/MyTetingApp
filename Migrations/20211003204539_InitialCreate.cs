using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTetingApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoneyTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumCode = table.Column<int>(type: "int", nullable: false),
                    CharCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nominal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    System_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValCurs_Id = table.Column<int>(type: "int", nullable: false),
                    DateOfUpload = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTable", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTable");
        }
    }
}
