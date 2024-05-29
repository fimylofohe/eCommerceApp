using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret_API.Migrations
{
    /// <inheritdoc />
    public partial class firstE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CargoCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoCompany",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CargoCompany",
                table: "Orders");
        }
    }
}
