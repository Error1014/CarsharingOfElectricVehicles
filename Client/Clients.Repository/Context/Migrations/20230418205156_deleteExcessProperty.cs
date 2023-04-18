using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clients.Repository.Migrations
{
    /// <inheritdoc />
    public partial class deleteExcessProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "DateRegistration",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Client",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegistration",
                table: "Client",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
