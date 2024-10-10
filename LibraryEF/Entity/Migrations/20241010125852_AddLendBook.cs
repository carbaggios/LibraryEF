using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddLendBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TermLendDays",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LendBook",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermLendDays = table.Column<int>(type: "int", nullable: false),
                    TakenDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_LendBook", x => x.Id);
                    table.ForeignKey(
                        name: "fk_LendBookBook",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_LendBookReader",
                        column: x => x.ReaderId,
                        principalTable: "Reader",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LendBook_BookId",
                table: "LendBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_LendBook_ReaderId",
                table: "LendBook",
                column: "ReaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LendBook");

            migrationBuilder.DropColumn(
                name: "TermLendDays",
                table: "Book");
        }
    }
}
