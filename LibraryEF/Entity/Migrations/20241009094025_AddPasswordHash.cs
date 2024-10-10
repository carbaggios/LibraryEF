using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Reader");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Librarian");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Reader",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Reader",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Librarian",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Librarian",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Librarian_Login",
                table: "Librarian",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Librarian_Login",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Reader");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Reader");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Librarian");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Librarian");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Reader",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Librarian",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
