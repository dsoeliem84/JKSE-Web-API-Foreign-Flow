using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForeignFlow",
                columns: table => new
                {
                    DateData = table.Column<DateTime>(type: "Date", nullable: false),
                    TickerCode = table.Column<string>(type: "Varchar(4)", nullable: false),
                    TypeFlow = table.Column<int>(type: "int", nullable: false),
                    VolumeTotal = table.Column<int>(type: "int", nullable: false),
                    ValueTotal = table.Column<long>(type: "bigint", nullable: false),
                    VolumeBuy = table.Column<int>(type: "int", nullable: false),
                    VolumeSell = table.Column<int>(type: "int", nullable: false),
                    NetRatioVolume = table.Column<decimal>(type: "Decimal (8,2)", nullable: false),
                    DominationRatio = table.Column<decimal>(type: "Decimal (8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignFlow", x => new { x.DateData, x.TickerCode });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForeignFlow");
        }
    }
}
