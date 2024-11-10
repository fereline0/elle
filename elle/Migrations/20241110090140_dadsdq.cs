using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elle.Migrations
{
    /// <inheritdoc />
    public partial class dadsdq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Immovables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Immovables",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
