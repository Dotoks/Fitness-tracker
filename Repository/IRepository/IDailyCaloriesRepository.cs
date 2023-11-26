using Fitness_Tracker.Models;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IDailyMacrosRepository : IRepository<DailyMacros>
    {
        void Update(DailyMacros obj);
    }
}
