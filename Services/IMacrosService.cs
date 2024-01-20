using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels.Calories;

namespace Fitness_Tracker.Services
{
    public interface IMacrosService
    {
        Task CreateAsync(string userId, decimal weight, decimal height, int age, string acitivtyLevel, string gender);

        Task UpdateMacrosId(int bodyId);
        Task UpdateDailyMacros(UpdateMacrosInputModel input, string userId);

        DailyMacros GetMacros<DailyMacros>(string userId);
    }
}
