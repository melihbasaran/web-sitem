using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YemekAsistani.Migrations
{
    /// <inheritdoc />
    public partial class AlisverisSahibi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "ShoppingItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ShoppingItems");
        }
    }
}
