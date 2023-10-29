using Fitness_Tracker.Models;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        void Update(Recipe obj);
    }
}
