using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marathons_Platfom.Core.Migrations
{
    /// <inheritdoc />
    public partial class AdminStatusRemovedFromUset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminStatus",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminStatus",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
