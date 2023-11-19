using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

namespace Fitness_Tracker.Repository
{
    public class DailyCaloriesRepository : Repository<DailyCalories>, IDailyCaloriesRepository
    {
        private ApplicationDbContext _db;
        public DailyCaloriesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(DailyCalories obj)
        {
            _db.DailyCalories.Update(obj);
        }
    }
}
