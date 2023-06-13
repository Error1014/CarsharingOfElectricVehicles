using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clients.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Client",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
