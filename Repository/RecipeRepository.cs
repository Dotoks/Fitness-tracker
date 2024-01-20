using Fitness_Tracker.Data;
using Fitness_Tracker.HelperClassesForRecipes;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var regex = new Regex(@"(\d+)\s*hrs?\s*(\d*)\s*mins");

            var match = regex.Match(cookingTime);

            if (match.Success)
            {
                var hours = int.Parse(match.Groups[1].Value);
                var minutes = string.IsNullOrEmpty(match.Groups[2].Value) ? 0 : int.Parse(match.Groups[2].Value);

                return hours * 60 + minutes;
            }

            return 0;
        }
        private List<Recipe> CookingTimeInRange(List<Recipe> recipes, TimeRange range)
        {
            var filteredRecipes = new List<Recipe>();

            foreach (var recipe in recipes)
            {
                var totalMinutes = ParseCookingTimeToMinutes(recipe.CookingTime);

                //if (totalMinutes == 0 || (range.MinMinutes == 0 && range.MinHours == 0 && range.MaxMinutes == 0 && range.MaxHours == 0))
                //{
                //    filteredRecipes.Add(recipe);
                //}
                //else
                //{
                    var minTotalMinutes = range.MinHours * 60 + range.MinMinutes;
                    var maxTotalMinutes = range.MaxHours * 60 + range.MaxMinutes;

                    if (minTotalMinutes <= totalMinutes && totalMinutes <= maxTotalMinutes)
                    {
                        filteredRecipes.Add(recipe);
                    }
               // }
            }

            return filteredRecipes;
        }
        public IEnumerable<Recipe> Filter(List<string>? ingredientsFilter, TimeRange? cookingTimeFilter, string? recipeNameFilter, int? caloriesMinFilter, int? caloriesMaxFilter, int? carbsFilter, int? proteinFilter, int? fatsFilter)
        {
            var filteredRecipes = _db.Recipes.Include(r => r.Macros).ThenInclude(ri => ri.Ingredient).Include(r => r.PreparationInstructions).AsQueryable();

            if (ingredientsFilter != null && ingredientsFilter.Any() && ingredientsFilter.ElementAt(0) != null)
            {
                foreach (var ingredient in ingredientsFilter)
                {
                    ingredient.ToLower();
                    filteredRecipes = filteredRecipes
                        .Where(r => r.Macros.Any(m => m.Ingredient.IngredientName.Contains(ingredient)));
                }

              
            }
            
            

            if (!string.IsNullOrEmpty(recipeNameFilter))
            {
                recipeNameFilter.ToLower();
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

           // filteredRecipes.Include(r => r.PreparationInstructions);
            var filteredRecipesList = filteredRecipes.ToList();

            if (carbsFilter.HasValue && carbsFilter > 0)
            {
                filteredRecipesList = filteredRecipesList
                    .Where(r => r.Carbohydrates <= carbsFilter).ToList();
            }

            if (proteinFilter.HasValue && proteinFilter > 0)
            {
                filteredRecipesList = filteredRecipesList
                    .Where(r => r.Proteins <= proteinFilter).ToList();
            }

            if (fatsFilter.HasValue && fatsFilter > 0)
            {
                filteredRecipesList = filteredRecipesList
                    .Where(r => r.Fats <= fatsFilter).ToList();
            }


            if (cookingTimeFilter != null && cookingTimeFilter.MinMinutes != null && cookingTimeFilter.MinHours != null && cookingTimeFilter.MaxMinutes != null && cookingTimeFilter.MaxHours != null)
            {
                if (cookingTimeFilter.MinMinutes != 0 && cookingTimeFilter.MinHours != 0 && cookingTimeFilter.MaxMinutes != 0 && cookingTimeFilter.MaxHours != 0)
                {

                    filteredRecipesList = CookingTimeInRange(filteredRecipesList, cookingTimeFilter);
                }
            }
            return filteredRecipesList;
        }
        public List<Recipe> GetRecipes()
        {
            return _db.Recipes.ToList(); 
        }

        public Recipe GetRecipeById(int recipeId)
        {
            return _db.Recipes.FirstOrDefault(r => r.RecipeID == recipeId);
        }
    }
}
