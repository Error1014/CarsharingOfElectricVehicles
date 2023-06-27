using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixIsEmailConfirmed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isEmailСonfirmed",
                table: "User",
                newName: "IsEmailСonfirmed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEmailСonfirmed",
                table: "User",
                newName: "isEmailСonfirmed");
        }
    }
}
