using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marathons_Platfom.Core.Migrations
{
    /// <inheritdoc />
    public partial class UserExerciseStatusesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Exercises");

            migrationBuilder.CreateTable(
                name: "UserExerciseStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExerciseStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExerciseStatus_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExerciseStatus_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserExerciseStatus_ExerciseId",
                table: "UserExerciseStatus",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExerciseStatus_UserId",
                table: "UserExerciseStatus",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExerciseStatus");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Exercises",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
