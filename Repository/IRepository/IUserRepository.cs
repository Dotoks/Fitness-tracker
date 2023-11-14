using Fitness_Tracker.Models;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Detach(User entity);
        void Update(User User);
    }
}
