using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddingStudentAndInstructorRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_instructors_StudentId",
                table: "instructors",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_students_StudentId",
                table: "instructors",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_students_StudentId",
                table: "instructors");

            migrationBuilder.DropIndex(
                name: "IX_instructors_StudentId",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "instructors");
        }
    }
}
