using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyStats",
                columns: table => new
                {
                    DateData = table.Column<DateTime>(type: "Date", nullable: false),
                    IndexPrice = table.Column<decimal>(type: "Decimal (8,3)", nullable: false),
                    VolumeTransaction = table.Column<decimal>(type: "Decimal (8,3)", nullable: false),
                    Turnover = table.Column<decimal>(type: "Decimal (8,3)", nullable: false),
                    ForeignNetBuy = table.Column<decimal>(type: "Decimal (8,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyStats", x => x.DateData);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyStats");
        }
    }
}
