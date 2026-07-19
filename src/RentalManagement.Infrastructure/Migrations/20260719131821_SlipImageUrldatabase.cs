using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SlipImageUrldatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SlipImageUrl",
                table: "Payments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlipImageUrl",
                table: "Payments");
        }
    }
}
