using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret_API.Migrations
{
    /// <inheritdoc />
    public partial class firstD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Comments");
        }
    }
}
