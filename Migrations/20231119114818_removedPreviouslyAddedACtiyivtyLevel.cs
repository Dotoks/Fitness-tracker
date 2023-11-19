using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class removedPreviouslyAddedACtiyivtyLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityLevel",
                table: "DailyCalories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivityLevel",
                table: "DailyCalories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
