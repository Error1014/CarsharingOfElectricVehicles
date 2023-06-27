using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixIsActiv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActiv",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActiv",
                table: "User");
        }
    }
}
