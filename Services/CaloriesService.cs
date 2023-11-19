using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels.Calories;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Services
{
    public class CaloriesService : ICaloriesService
    {
        private readonly ApplicationDbContext _context;

        public CaloriesService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(string userId, decimal weight, decimal height, int age, string acitivtyLevel)
        {

            var currentBody = _context.Body.FirstOrDefault(x => x.UserID == userId);

            double BMR = (double)((10 * weight) + ((decimal)6.25 * height) - (5 * age) + 5);

            switch (acitivtyLevel)
            {
                case "BMR":
                    BMR *= 1;
                    break;
                case "NoActivity":
                    BMR *= 1.2;
                        break;
                case "LightActivity":
                    BMR *= 1.375;
                    break;
                case "ModerateActivity":
                    BMR *= 1.55;
                    break;
                case "VeryActivity":
                    BMR *= 1.725;
                    break;
                case "ExtraActivity":
                    BMR *= 1.9;
                    break;
            }

            var DailyCalories = new DailyCalories
            {
                BodyId = currentBody.BodyID,
                CaloriesRecommended = (int)BMR,
                CaloriesConsumed = 0

            };

            await _context.AddAsync(DailyCalories);
            await _context.SaveChangesAsync();
        }

        public DailyCalories GetCalories<DailyCalories>(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDailyCalories(UpdateCaloriesInputModel input, string userId)
        {
            var today = DateTime.Today;

            var existingBodyInfo = _context.Body.Where(x => x.UserID == userId && x.EffectiveThroughDate == DateTime.MaxValue)
                .OrderByDescending(x => x.EffectiveFromDate)
                .FirstOrDefault();

            var existingEntry = _context.DailyCalories.FirstOrDefault(x => x.BodyId == existingBodyInfo.BodyID);


           throw new NotImplementedException();

        }
    }
}
