using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class DbSetsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_RecipeScraped_RecipeScrapedID",
                table: "Instruction");

            migrationBuilder.DropForeignKey(
                name: "FK_Macros_RecipeScraped_RecipeScrapedID",
                table: "Macros");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeScraped_UserScraped_CreatedBy",
                table: "RecipeScraped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserScraped",
                table: "UserScraped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeScraped",
                table: "RecipeScraped");

            migrationBuilder.RenameTable(
                name: "UserScraped",
                newName: "UsersScraped");

            migrationBuilder.RenameTable(
                name: "RecipeScraped",
                newName: "RecipesScraped");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeScraped_CreatedBy",
                table: "RecipesScraped",
                newName: "IX_RecipesScraped_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersScraped",
                table: "UsersScraped",
                column: "UserScrapedID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipesScraped",
                table: "RecipesScraped",
                column: "RecipeScrapedID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_RecipesScraped_RecipeScrapedID",
                table: "Instruction",
                column: "RecipeScrapedID",
                principalTable: "RecipesScraped",
                principalColumn: "RecipeScrapedID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Macros_RecipesScraped_RecipeScrapedID",
                table: "Macros",
                column: "RecipeScrapedID",
                principalTable: "RecipesScraped",
                principalColumn: "RecipeScrapedID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesScraped_UsersScraped_CreatedBy",
                table: "RecipesScraped",
                column: "CreatedBy",
                principalTable: "UsersScraped",
                principalColumn: "UserScrapedID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_RecipesScraped_RecipeScrapedID",
                table: "Instruction");

            migrationBuilder.DropForeignKey(
                name: "FK_Macros_RecipesScraped_RecipeScrapedID",
                table: "Macros");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipesScraped_UsersScraped_CreatedBy",
                table: "RecipesScraped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersScraped",
                table: "UsersScraped");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipesScraped",
                table: "RecipesScraped");

            migrationBuilder.RenameTable(
                name: "UsersScraped",
                newName: "UserScraped");

            migrationBuilder.RenameTable(
                name: "RecipesScraped",
                newName: "RecipeScraped");

            migrationBuilder.RenameIndex(
                name: "IX_RecipesScraped_CreatedBy",
                table: "RecipeScraped",
                newName: "IX_RecipeScraped_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserScraped",
                table: "UserScraped",
                column: "UserScrapedID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeScraped",
                table: "RecipeScraped",
                column: "RecipeScrapedID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeScraped_UserScraped_CreatedBy",
                table: "RecipeScraped",
                column: "CreatedBy",
                principalTable: "UserScraped",
                principalColumn: "UserScrapedID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
