using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class FixingRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeScrapedID",
                table: "Macros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeScrapedID",
                table: "Instruction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserScraped",
                columns: table => new
                {
                    UserScrapedID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScraped", x => x.UserScrapedID);
                });

            migrationBuilder.CreateTable(
                name: "RecipeScraped",
                columns: table => new
                {
                    RecipeScrapedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookingTime = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeScraped", x => x.RecipeScrapedID);
                    table.ForeignKey(
                        name: "FK_RecipeScraped_UserScraped_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "UserScraped",
                        principalColumn: "UserScrapedID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Macros_RecipeScrapedID",
                table: "Macros",
                column: "RecipeScrapedID");

            migrationBuilder.CreateIndex(
                name: "IX_Instruction_RecipeScrapedID",
                table: "Instruction",
                column: "RecipeScrapedID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeScraped_CreatedBy",
                table: "RecipeScraped",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_RecipeScraped_RecipeScrapedID",
                table: "Instruction",
                column: "RecipeScrapedID",
                principalTable: "RecipeScraped",
                principalColumn: "RecipeScrapedID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Macros_RecipeScraped_RecipeScrapedID",
                table: "Macros",
                column: "RecipeScrapedID",
                principalTable: "RecipeScraped",
                principalColumn: "RecipeScrapedID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_RecipeScraped_RecipeScrapedID",
                table: "Instruction");

            migrationBuilder.DropForeignKey(
                name: "FK_Macros_RecipeScraped_RecipeScrapedID",
                table: "Macros");

            migrationBuilder.DropTable(
                name: "RecipeScraped");

            migrationBuilder.DropTable(
                name: "UserScraped");

            migrationBuilder.DropIndex(
                name: "IX_Macros_RecipeScrapedID",
                table: "Macros");

            migrationBuilder.DropIndex(
                name: "IX_Instruction_RecipeScrapedID",
                table: "Instruction");

            migrationBuilder.DropColumn(
                name: "RecipeScrapedID",
                table: "Macros");

            migrationBuilder.DropColumn(
                name: "RecipeScrapedID",
                table: "Instruction");
        }
    }
}
