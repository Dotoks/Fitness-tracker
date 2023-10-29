using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

namespace Fitness_Tracker.Repository
{
    public class BodyRepository: Repository<Body>, IBodyRepository
    {
        private ApplicationDbContext _db;
        public BodyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Body obj)
        {
            _db.Bodies.Update(obj);
        }
    }
}
