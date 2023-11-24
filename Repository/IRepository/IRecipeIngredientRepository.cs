using Fitness_Tracker.Models;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IRecipeIngredientRepository : IRepository<RecipeIngredient>
    {
        void Update(RecipeIngredient obj);
    }
}
