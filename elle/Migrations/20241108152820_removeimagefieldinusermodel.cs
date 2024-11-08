using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elle.Migrations
{
    /// <inheritdoc />
    public partial class removeimagefieldinusermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
