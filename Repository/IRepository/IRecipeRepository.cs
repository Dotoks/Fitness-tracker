using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Fitness_Tracker.HelperClassesForRecipes;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        void Update(Recipe obj);

        public IEnumerable<Recipe> FilterByIngredient(List<string>? ingredientsFilter, TimeRange? cookingTimeFilter, string? recipeNameFilter);
    }
}
//[Key]
//public int RecipeID { get; set; }

//[Required]
//public string RecipeName { get; set; }//done 

//[Required]
//public string Description { get; set; }//done 

//[Required]
//public ICollection<Instruction> PreparationInstructions { get; set; }//done 



//public string CookingTime { get; set; }//done 

//public int Servings { get; set; }//done 

//public string? DifficultyLevel { get; set; }//done 

//public DateTime CreatedDate { get; set; }//done 

//[Required]

//public string CreatedBy { get; set; } // Foreign Key referencing UserID
//[ForeignKey("CreatedBy")]
//public User Creator { get; set; }//done 

//public ICollection<Macro> Macros { get; set; } // One-to-many relationship via the junction table