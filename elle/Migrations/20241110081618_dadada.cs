using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elle.Migrations
{
    /// <inheritdoc />
    public partial class dadada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentStartDate",
                table: "Immovables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RentStartDate",
                table: "Immovables",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
