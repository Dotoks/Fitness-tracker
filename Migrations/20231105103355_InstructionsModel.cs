using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class InstructionsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreparationInstructions",
                table: "Recipes");

           

            migrationBuilder.RenameColumn(
                name: "DateMeasurment",
                table: "Bodies",
                newName: "EffectiveThroughDate");

            migrationBuilder.AddColumn<bool>(
                name: "CurrentRecordIndicator",
                table: "Bodies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveFromDate",
                table: "Bodies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Instructions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_RecipeId",
                table: "Instructions",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropColumn(
                name: "CurrentRecordIndicator",
                table: "Bodies");

            migrationBuilder.DropColumn(
                name: "EffectiveFromDate",
                table: "Bodies");

            migrationBuilder.RenameColumn(
                name: "EffectiveThroughDate",
                table: "Bodies",
                newName: "DateMeasurment");

            migrationBuilder.AddColumn<string>(
                name: "PreparationInstructions",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
