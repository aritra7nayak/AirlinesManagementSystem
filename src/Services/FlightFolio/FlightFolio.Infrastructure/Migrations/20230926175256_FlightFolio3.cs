using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightFolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FlightFolio3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassengerCount",
                table: "AeroplaneFolios",
                newName: "TotalPassenger");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPassenger",
                table: "AeroplaneFolios",
                newName: "PassengerCount");
        }
    }
}
