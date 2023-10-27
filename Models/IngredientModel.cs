using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; }

        [Required]
        public string IngredientName { get; set; }

        public string Category { get; set; }

        public ICollection<Macro> Macros { get; set; } // One-to-many relationship
    }
}