using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marathons_Platfom.Core.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleInMarathonAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleInMarathon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MarathonId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleInMarathon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleInMarathon_Marathons_MarathonId",
                        column: x => x.MarathonId,
                        principalTable: "Marathons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleInMarathon_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleInMarathon_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleInMarathon_MarathonId",
                table: "UserRoleInMarathon",
                column: "MarathonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleInMarathon_RoleId",
                table: "UserRoleInMarathon",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleInMarathon_UserId",
                table: "UserRoleInMarathon",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoleInMarathon");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
