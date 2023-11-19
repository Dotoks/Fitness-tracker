using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels.Calories;

namespace Fitness_Tracker.Services
{
    public interface ICaloriesService
    {
        Task CreateAsync(string userId, decimal weight, decimal height, int age, string acitivtyLevel);
        Task UpdateDailyCalories(UpdateCaloriesInputModel input, string userId);

        DailyCalories GetCalories<DailyCalories>(string userId);
    }
}
