using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

namespace Fitness_Tracker.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IBodyRepository Body { get; private set; }

        public IIngredientRepository Ingredient { get; private set; }

        public IRecipeIngredientRepository RecipeIngredient { get; private set; }

        public IRecipeRepository Recipe { get; private set; }

        public IUserRepository User { get; private set; }
        public IInstructionRepository Instruction { get; private set; }
        public IDailyCaloriesRepository DailyCalories { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Body = new BodyRepository(_db);
            Ingredient = new IngredientRepository(_db);
            Instruction = new InstructionRepository(_db);
            DailyCalories = new DailyCaloriesRepository(_db);
            RecipeIngredient = new RecipeIngredientRepository(_db);
            Recipe = new RecipeRepository(_db);
            User = new UserRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
