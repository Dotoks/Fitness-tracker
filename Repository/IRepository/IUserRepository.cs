using Fitness_Tracker.Models;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User User);
    }
}
