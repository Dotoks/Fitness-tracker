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
        public ICollection<Instructions> PreparationInstructions { get; set; }



        public int CookingTime { get; set; }

        public int Servings { get; set; }//todo

        public string? DifficultyLevel { get; set; }//can't scrape because there is nothing in the allrecipes website about difficulty

        public DateTime CreatedDate { get; set; }//todo

        [Required]
        
        public string CreatedBy { get; set; } // Foreign Key referencing UserID
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }//todo

        public ICollection<Macro> Macros { get; set; } // One-to-many relationship via the junction table
    }
}
