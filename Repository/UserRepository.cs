using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

namespace Fitness_Tracker.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(User user)
        {
            _db.Users.Update(user);
        }
    }
}
