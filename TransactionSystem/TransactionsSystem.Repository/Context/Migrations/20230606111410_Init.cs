using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionsSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTransaction", x => x.Id);
                });
            migrationBuilder.InsertData(
                table: "TypeTransaction",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Пополнение баланса" });
            migrationBuilder.InsertData(
               table: "TypeTransaction",
               columns: new[] { "Id", "Name" },
               values: new object[] { 2, "Кэшбэк" });
            migrationBuilder.InsertData(
                table: "TypeTransaction",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Оплата аренды" });
            migrationBuilder.InsertData(
                table: "TypeTransaction",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Оплата штрафа" });
            migrationBuilder.InsertData(
                table: "TypeTransaction",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Приобретение абонимента" });
            migrationBuilder.CreateTable(
                name: "TransactionItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Summ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeTransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionItem_TypeTransaction_TypeTransactionId",
                        column: x => x.TypeTransactionId,
                        principalTable: "TypeTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItem_TypeTransactionId",
                table: "TransactionItem",
                column: "TypeTransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionItem");

            migrationBuilder.DropTable(
                name: "TypeTransaction");
        }
    }
}
