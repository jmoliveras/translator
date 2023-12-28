using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Translator.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "translations",
                newName: "Result");

            migrationBuilder.AddColumn<string>(
                name: "DetectedLanguage",
                table: "translations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalText",
                table: "translations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "translations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetectedLanguage",
                table: "translations");

            migrationBuilder.DropColumn(
                name: "OriginalText",
                table: "translations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "translations");

            migrationBuilder.RenameColumn(
                name: "Result",
                table: "translations",
                newName: "Text");
        }
    }
}
