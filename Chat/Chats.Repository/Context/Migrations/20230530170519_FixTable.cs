using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chats.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameClient",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatronymicClient",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurnameClient",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameClient",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "PatronymicClient",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SurnameClient",
                table: "Chats");
        }
    }
}
