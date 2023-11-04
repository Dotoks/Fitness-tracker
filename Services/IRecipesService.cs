using Fitness_Tracker.ViewModels.Recipes;

namespace Fitness_Tracker.Services
{
    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input, string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        IEnumerable<T> GetAllBySearch<T>(string SearchTerm, int page, int itemsPerPage = 12);

        IEnumerable<T> GetRecipes<T>(int count);

        IEnumerable<T> GetRecipesRandom<T>(int count);

        IEnumerable<T> GetRecipesForUser<T>(string userId, int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditRecipeInputModel input);

        Task DeleteAsync(int id);
    }
}
