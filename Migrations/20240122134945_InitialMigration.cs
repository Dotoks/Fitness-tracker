using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           


            migrationBuilder.CreateTable(
                name: "DailyMacros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyId = table.Column<int>(type: "int", nullable: false),
                    CaloriesConsumed = table.Column<int>(type: "int", nullable: false),
                    CaloriesRecommended = table.Column<int>(type: "int", nullable: false),
                    CarbohydratesConsumed = table.Column<int>(type: "int", nullable: false),
                    CarbohydratesRecommended = table.Column<int>(type: "int", nullable: false),
                    ProteinsConsumed = table.Column<int>(type: "int", nullable: false),
                    ProteinsRecommended = table.Column<int>(type: "int", nullable: false),
                    FatsConsumed = table.Column<int>(type: "int", nullable: false),
                    FatsRecommended = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMacros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyMacros_Bodies_BodyId",
                        column: x => x.BodyId,
                        principalTable: "Bodies",
                        principalColumn: "BodyID",
                        onDelete: ReferentialAction.Cascade);
                });


          

           
            migrationBuilder.CreateIndex(
                name: "IX_DailyMacros_BodyId",
                table: "DailyMacros",
                column: "BodyId",
                unique: true);

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "DailyMacros");

           

        }
    }
}
