using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; } // Primary Key

        public required string IngredientName { get; set; }

        public string? Category { get; set; }

        public required List<Macro> Macros { get; set; } // Many-to-many relationship via the junction table
    }
}