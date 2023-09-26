using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightFolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FlightFolio2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassengerCount",
                table: "AeroplaneFolios",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassengerCount",
                table: "AeroplaneFolios");
        }
    }
}
