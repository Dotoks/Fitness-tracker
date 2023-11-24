using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

namespace Fitness_Tracker.Repository
{
    public class RecipeIngredientRepository : Repository<RecipeIngredient>, IRecipeIngredientRepository
    {
        private ApplicationDbContext _db;
        public RecipeIngredientRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(RecipeIngredient obj)
        {
            _db.RecipeIngredients.Update(obj);
        }
    }
}
