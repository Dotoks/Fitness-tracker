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
        public IEnumerable<Recipe> FilterByIngredient(List<string>? ingredientsFilter, TimeRange? cookingTimeFilter, string? recipeNameFilter)
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
                filteredRecipes = filteredRecipes.Where(r => ingredientsFilter.All(ingredient => r.Macros.Any(m => m.Ingredient.IngredientName.Contains(ingredient))));
            }

            if (cookingTimeFilter.MinMinutes!=0 && cookingTimeFilter.MinHours !=0 && cookingTimeFilter.MaxMinutes !=0 && cookingTimeFilter.MaxHours !=0)
            {
                filteredRecipes = filteredRecipes
                    .Where(r => CookingTimeInRange(r.CookingTime, cookingTimeFilter));
            }

            if (!string.IsNullOrEmpty(recipeNameFilter))
            {
                filteredRecipes = filteredRecipes
                    .Where(r => r.RecipeName.Contains(recipeNameFilter));
            }

            return filteredRecipes.ToList();
           
        }
    }
}
