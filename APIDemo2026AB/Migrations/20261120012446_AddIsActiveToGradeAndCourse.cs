using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDemo2026AB.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToGradeAndCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Grades",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Courses");
        }
    }
}
