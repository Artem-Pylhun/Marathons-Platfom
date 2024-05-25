using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marathons_Platfom.Core.Migrations
{
    /// <inheritdoc />
    public partial class BadgeMarathonsTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BadgeMarathon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BadgeId = table.Column<int>(type: "int", nullable: false),
                    MarathonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadgeMarathon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BadgeMarathon_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BadgeMarathon_Marathons_MarathonId",
                        column: x => x.MarathonId,
                        principalTable: "Marathons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BadgeMarathon_BadgeId",
                table: "BadgeMarathon",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_BadgeMarathon_MarathonId",
                table: "BadgeMarathon",
                column: "MarathonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BadgeMarathon");
        }
    }
}
