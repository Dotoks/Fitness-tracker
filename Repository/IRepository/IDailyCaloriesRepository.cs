using Fitness_Tracker.Models;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IDailyCaloriesRepository : IRepository<DailyCalories>
    {
        void Update(DailyCalories obj);
    }
}
