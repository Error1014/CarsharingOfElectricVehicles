using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rents.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixRentKilometr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KilometersOutsideTariff",
                table: "Rents",
                newName: "Kilometers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kilometers",
                table: "Rents",
                newName: "KilometersOutsideTariff");
        }
    }
}
