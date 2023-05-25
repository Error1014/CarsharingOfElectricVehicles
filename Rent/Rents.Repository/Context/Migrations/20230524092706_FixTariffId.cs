using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rents.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixTariffId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Tariffs_TariffId",
                table: "Rents");

            migrationBuilder.AlterColumn<Guid>(
                name: "TariffId",
                table: "Rents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Tariffs_TariffId",
                table: "Rents",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Tariffs_TariffId",
                table: "Rents");

            migrationBuilder.AlterColumn<Guid>(
                name: "TariffId",
                table: "Rents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Tariffs_TariffId",
                table: "Rents",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
