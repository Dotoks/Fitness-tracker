using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

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
    }
}
