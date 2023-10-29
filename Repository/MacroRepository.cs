using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

namespace Fitness_Tracker.Repository
{
    public class MacroRepository : Repository<Macro>, IMacroRepository
    {
        private ApplicationDbContext _db;
        public MacroRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Macro obj)
        {
            _db.Macros.Update(obj);
        }
    }
}
