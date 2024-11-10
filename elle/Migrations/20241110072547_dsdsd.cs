using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elle.Migrations
{
    /// <inheritdoc />
    public partial class dsdsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Immovables_Homes_HomeId",
                table: "Immovables");

            migrationBuilder.AlterColumn<int>(
                name: "HomeId",
                table: "Immovables",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Immovables_Homes_HomeId",
                table: "Immovables",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Immovables_Homes_HomeId",
                table: "Immovables");

            migrationBuilder.AlterColumn<int>(
                name: "HomeId",
                table: "Immovables",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Immovables_Homes_HomeId",
                table: "Immovables",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id");
        }
    }
}
