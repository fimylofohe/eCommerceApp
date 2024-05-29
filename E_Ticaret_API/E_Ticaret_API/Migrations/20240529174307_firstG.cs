using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret_API.Migrations
{
    /// <inheritdoc />
    public partial class firstG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeoURL",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoURL",
                table: "Products");
        }
    }
}
