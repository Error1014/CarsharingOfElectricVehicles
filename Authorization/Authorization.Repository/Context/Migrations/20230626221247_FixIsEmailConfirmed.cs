using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixIsEmailConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isActiv",
                table: "User",
                newName: "isEmailСonfirmed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isEmailСonfirmed",
                table: "User",
                newName: "isActiv");
        }
    }
}
