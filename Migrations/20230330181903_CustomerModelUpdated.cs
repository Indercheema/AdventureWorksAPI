using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventureWorksAPI.Migrations
{
    /// <inheritdoc />
    public partial class CustomerModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                schema: "SalesLT",
                table: "Customer",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                comment: "Random value concatenated with the password string before the password is hashed.",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldComment: "Random value concatenated with the password string before the password is hashed.");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                schema: "SalesLT",
                table: "Customer",
                type: "varchar(128)",
                unicode: false,
                maxLength: 128,
                nullable: true,
                comment: "Password for the e-mail account.",
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldUnicode: false,
                oldMaxLength: 128,
                oldComment: "Password for the e-mail account.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                schema: "SalesLT",
                table: "Customer",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                comment: "Random value concatenated with the password string before the password is hashed.",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true,
                oldComment: "Random value concatenated with the password string before the password is hashed.");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                schema: "SalesLT",
                table: "Customer",
                type: "varchar(128)",
                unicode: false,
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                comment: "Password for the e-mail account.",
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldUnicode: false,
                oldMaxLength: 128,
                oldNullable: true,
                oldComment: "Password for the e-mail account.");
        }
    }
}
