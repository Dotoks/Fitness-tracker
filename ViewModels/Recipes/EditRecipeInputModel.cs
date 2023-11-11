using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Microsoft.AspNetCore.Http;

namespace Fitness_Tracker.ViewModels.Recipes
{
    public class EditRecipeInputModel
    {
        [Required]
        public string RecipeName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PreparationInstructions { get; set; }

        [Required]
        public int CookingTime { get; set; }

        public int Servings { get; set; }

        public string DifficultyLevel { get; set; }
    }
}
