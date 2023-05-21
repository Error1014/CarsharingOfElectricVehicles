using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clients.Repository.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSubscrible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientSubscription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientSubscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateSubscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityMonths = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSubscription_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientSubscription_ClientId",
                table: "ClientSubscription",
                column: "ClientId");
        }
    }
}
