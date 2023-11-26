using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

namespace Fitness_Tracker.Repository
{
    public class DailyMacrosRepository : Repository<DailyMacros>, IDailyMacrosRepository
    {
        private ApplicationDbContext _db;
        public DailyMacrosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(DailyMacros obj)
        {
            _db.DailyMacros.Update(obj);
        }
    }
}
