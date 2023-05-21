using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clients.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddClientSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientSubscriptionId",
                table: "Client",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientSubscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateSubscription = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSubscription", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_ClientSubscriptionId",
                table: "Client",
                column: "ClientSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_ClientSubscription_ClientSubscriptionId",
                table: "Client",
                column: "ClientSubscriptionId",
                principalTable: "ClientSubscription",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_ClientSubscription_ClientSubscriptionId",
                table: "Client");

            migrationBuilder.DropTable(
                name: "ClientSubscription");

            migrationBuilder.DropIndex(
                name: "IX_Client_ClientSubscriptionId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientSubscriptionId",
                table: "Client");
        }
    }
}
