using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characteristics_CarId",
                table: "Characteristics");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CarId",
                table: "Characteristics",
                column: "CarId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characteristics_CarId",
                table: "Characteristics");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CarId",
                table: "Characteristics",
                column: "CarId");
        }
    }
}
