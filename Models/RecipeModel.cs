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
        public string RecipeName { get; set; }//done 

        [Required]
        public string Description { get; set; }//done 

        [Required]
        public ICollection<Instruction> PreparationInstructions { get; set; }//done 


        public decimal Calories { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Proteins { get; set; }
        public decimal Fats { get; set; }







        public string CookingTime { get; set; }//done 

        public int Servings { get; set; }//done 

        public string? DifficultyLevel { get; set; }//done 

        public DateTime CreatedDate { get; set; }//done 

        [Required]
        
        public string CreatedBy { get; set; } // Foreign Key referencing UserID
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }//done 

        //THIS SHOULD BE CALLED INGREDIENTS
        public ICollection<RecipeIngredient> Macros { get; set; } // One-to-many relationship via the junction table
    }
}
//RecipeName = scrapedName,
//CookingTime = scrapedCookingTime,
//CreatedDate = scrapedCreation,
//Description = scrapedDesc,
//Servings = scrapedServings,
//DifficultyLevel = null,
//Creator = user,