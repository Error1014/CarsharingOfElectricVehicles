using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rents.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentCheques_Bookings_RentId",
                table: "RentCheques");

            migrationBuilder.DropIndex(
                name: "IX_RentCheques_RentId",
                table: "RentCheques");

            migrationBuilder.RenameColumn(
                name: "DateTimeBeginRent",
                table: "Bookings",
                newName: "DateTimeBeginBoocking");

            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "RentCheques",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RentCheques_BookingId",
                table: "RentCheques",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentCheques_Bookings_BookingId",
                table: "RentCheques",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentCheques_Bookings_BookingId",
                table: "RentCheques");

            migrationBuilder.DropIndex(
                name: "IX_RentCheques_BookingId",
                table: "RentCheques");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "RentCheques");

            migrationBuilder.RenameColumn(
                name: "DateTimeBeginBoocking",
                table: "Bookings",
                newName: "DateTimeBeginRent");

            migrationBuilder.CreateIndex(
                name: "IX_RentCheques_RentId",
                table: "RentCheques",
                column: "RentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentCheques_Bookings_RentId",
                table: "RentCheques",
                column: "RentId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
