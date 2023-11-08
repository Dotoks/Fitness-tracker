using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class RecipeScraped
    {
        [Key]
        public int RecipeScrapedID { get; set; }

        [Required]
        public string RecipeName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ICollection<Instruction> PreparationInstructions { get; set; }



        public int CookingTime { get; set; }

        public int Servings { get; set; }

      

        public DateTime CreatedDate { get; set; }

        [Required]

        public long CreatedBy { get; set; } // Foreign Key referencing UserID
        [ForeignKey("CreatedBy")]
        public UserScraped Creator { get; set; }

        public ICollection<Macro> Macros { get; set; } // One-to-many relationship via the junction table
    }
}


// public string? DifficultyLevel { get; set; }//can't scrape because there is nothing in the allrecipes website about difficulty