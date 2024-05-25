using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marathons_Platfom.Core.Migrations
{
    /// <inheritdoc />
    public partial class TitleAddedToExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Exercises");
        }
    }
}
