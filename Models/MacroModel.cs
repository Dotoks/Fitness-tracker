using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Tracker.Models
{
    public class Macro
    {
        [Key]
        public int MacroID { get; set; } // Primary Key
        [ForeignKey("RecipeId")]
        public int RecipeID { get; set; } // Foreign Key referencing RecipeID
        [ForeignKey("IngredientId")]
        public int IngredientID { get; set; } // Foreign Key referencing IngredientID

        public decimal Quantity { get; set; }

        public decimal Calories { get; set; }

        public decimal Carbohydrates { get; set; }

        public decimal Proteins { get; set; }

        public decimal Fats { get; set; }

        public required Recipe Recipe { get; set; }

        public Ingredient? Ingredient { get; set; }
    }
}
