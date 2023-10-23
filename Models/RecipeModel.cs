using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Tracker.Models
{
    public class Recipe
    {
        [Required]
        public int RecipeID { get; set; } // Primary Key

        public required string RecipeName { get; set; }

        public required string Description { get; set; }

        public required string PreparationInstructions { get; set; }

        public int CookingTime { get; set; }

        public int Servings { get; set; }

        public string? DifficultyLevel { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public int CreatedBy { get; set; } // Foreign Key referencing UserID

        public required User Creator { get; set; }

        public List<Macro> Macros { get; set; } // Many-to-many relationship via the junction table
    }
}
