using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository;
using Fitness_Tracker.Repository.IRepository;
using Fitness_Tracker.ViewModels.Recipes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Fitness_Tracker.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly ApplicationDbContext _context;

        public RecipesService(
            ApplicationDbContext context)
        {
               _context = context;
        }

        public async Task CreateAsync(CreateRecipeInputModel input, string userId)
        {
            var recipe = new Recipe
            {
                RecipeName = input.RecipeName,
                Description = input.Description,
             //   PreparationInstructions = input.PreparationInstructions,
                CookingTime = input.CookingTime,
                Servings = input.Servings,
                DifficultyLevel = input.DifficultyLevel,
                CreatedDate = DateTime.Now,
                CreatedBy = userId
            };
            
            await _context.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllBySearch<T>(string SearchTerm, int page, int itemsPerPage = 12)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetRecipes<T>(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetRecipesForUser<T>(string userId, int page, int itemsPerPage = 12)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetRecipesRandom<T>(int count)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, EditRecipeInputModel input)
        {
            throw new NotImplementedException();
        }
    }
}
