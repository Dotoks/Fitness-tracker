using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

namespace Fitness_Tracker.Repository
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private ApplicationDbContext _db;
        public IngredientRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Ingredient obj)
        {
            _db.Ingredients.Update(obj);
        }
    }
}
