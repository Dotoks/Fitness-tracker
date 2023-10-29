using Fitness_Tracker.Models;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        void Update(Ingredient obj);
    }
}
