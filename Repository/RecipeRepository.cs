using Fitness_Tracker.Data;
using Fitness_Tracker.HelperClassesForRecipes;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Fitness_Tracker.Repository
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private ApplicationDbContext _db;
        public RecipeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Recipe obj)
        {
            _db.Recipes.Update(obj);
        }
        private int ParseCookingTimeToMinutes(string cookingTime)
        {
            var regex = new Regex(@"(\d+)\s*hrs\s*(\d*)\s*mins");

            var match = regex.Match(cookingTime);

            if (match.Success)
            {
                var hours = int.Parse(match.Groups[1].Value);
                var minutes = string.IsNullOrEmpty(match.Groups[2].Value) ? 0 : int.Parse(match.Groups[2].Value);

                return hours * 60 + minutes;
            }

            return 0;
        }
        private bool CookingTimeInRange(string cookingTime, TimeRange range)
        {
          
            var totalMinutes = ParseCookingTimeToMinutes(cookingTime);
            if (totalMinutes == 0)
            {
                return true; // No filter applied, consider it within the range
            }
            var minTotalMinutes = range.MinHours * 60 + range.MinMinutes;
            var maxTotalMinutes = range.MaxHours * 60 + range.MaxMinutes;

            return minTotalMinutes <= totalMinutes && totalMinutes <= maxTotalMinutes;
        }
        public IEnumerable<Recipe> Filter(List<string>? ingredientsFilter, TimeRange? cookingTimeFilter, string? recipeNameFilter, int? caloriesMinFilter, int? caloriesMaxFilter, int? carbsFilter, int? proteinFilter, int? fatsFilter)
        {
            var filteredRecipes = _db.Recipes.AsQueryable();

            if (ingredientsFilter != null && ingredientsFilter.Any())
            {
                foreach (var ingredient in ingredientsFilter)
                {
                    filteredRecipes = filteredRecipes
                        .Where(r => r.Macros.Any(m => m.Ingredient.IngredientName.Contains(ingredient)));
                }

                // Ensure that the recipe contains all specified ingredients
              //  filteredRecipes = filteredRecipes
               //     .Where(r => ingredientsFilter.All(ingredient => r.Macros.Any(m => m.Ingredient.IngredientName.Contains(ingredient))));
            }
            if (cookingTimeFilter != null && cookingTimeFilter.MinMinutes != null && cookingTimeFilter.MinHours != null && cookingTimeFilter.MaxMinutes != null && cookingTimeFilter.MaxHours != null)
            {
                if (cookingTimeFilter.MinMinutes != 0 && cookingTimeFilter.MinHours != 0 && cookingTimeFilter.MaxMinutes != 0 && cookingTimeFilter.MaxHours != 0)
                {

                    filteredRecipes = filteredRecipes
                        .Where(r => CookingTimeInRange(r.CookingTime, cookingTimeFilter));
                }
            }
            

            if (!string.IsNullOrEmpty(recipeNameFilter))
            {
                filteredRecipes = filteredRecipes
                    .Where(r => r.RecipeName.Contains(recipeNameFilter));
            }

            if (caloriesMinFilter.HasValue && caloriesMinFilter > 0)
            {
                filteredRecipes = filteredRecipes
                    .Where(r => r.Calories >= caloriesMinFilter);
            }

            if (caloriesMaxFilter.HasValue && caloriesMaxFilter > 0)
            {
                filteredRecipes = filteredRecipes
                    .Where(r => r.Calories <= caloriesMaxFilter);
            }

            if (carbsFilter.HasValue && carbsFilter > 0)
            {
                filteredRecipes = filteredRecipes
                    .Where(r => r.Carbohydrates <= carbsFilter);
            }

            if (proteinFilter.HasValue && proteinFilter > 0)
            {
                filteredRecipes = filteredRecipes
                    .Where(r => r.Proteins <= proteinFilter);
            }

            if (fatsFilter.HasValue && fatsFilter > 0)
            {
                filteredRecipes = filteredRecipes
                    .Where(r => r.Fats <= fatsFilter);
            }
            var filteredRecipesList = filteredRecipes.ToList();
            return filteredRecipesList;
        }
    }
}
