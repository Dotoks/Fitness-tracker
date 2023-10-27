using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Tracker.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }

        [Required]
        public string RecipeName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PreparationInstructions { get; set; }



        public int CookingTime { get; set; }

        public int Servings { get; set; }

        public string? DifficultyLevel { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        
        public int CreatedBy { get; set; } // Foreign Key referencing UserID
        [ForeignKey("CreatedBy")]
        public required User Creator { get; set; }

        public ICollection<Macro> Macros { get; set; } // One-to-many relationship via the junction table
    }
}
