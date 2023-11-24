using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class Ingredient //SCRAPED
    {
        [Key]
        public int IngredientID { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        public string IngredientName { get; set; }
        [Required]
        public string Unit { get; set; }

        public ICollection<RecipeIngredient> Macros { get; set; } // One-to-many relationship
    }
}