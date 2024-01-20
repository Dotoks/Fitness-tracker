using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Microsoft.AspNetCore.Http;

namespace Fitness_Tracker.ViewModels.Recipes
{
    public class CreateRecipeInputModel
    {
        [Required]
        public string RecipeName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PreparationInstructions { get; set; }

        [Required]
        public string CookingTime { get; set; }

        public int Servings { get; set; }

        public string DifficultyLevel { get; set; }

        public decimal Calories { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Proteins { get; set; }
        public decimal Fats { get; set; }
    }
}
