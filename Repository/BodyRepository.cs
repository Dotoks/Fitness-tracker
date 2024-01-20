using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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
        public Body GetUserBody(string userId)
        {
            return _db.Bodies.Include(b => b.DailyMacros).FirstOrDefault(b => b.UserID == userId && b.EffectiveThroughDate == DateTime.MaxValue);
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
