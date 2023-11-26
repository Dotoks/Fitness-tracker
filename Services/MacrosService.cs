using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels.Calories;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Services
{
    public class MacrosService : IMacrosService
    {
        private readonly ApplicationDbContext _context;

        public MacrosService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(string userId, decimal weight, decimal height, int age, string acitivtyLevel, string gender)
        {

            var currentBody = _context.Bodies.FirstOrDefault(x => x.UserID == userId);

            double BMR = 0;

            if (gender == "Male")
            {
                BMR = (double)((10 * weight) + ((decimal)6.25 * height) - (5 * age) + 5);
            }
            else if (gender == "Female")
            {
                BMR = (double)((10 * weight) + ((decimal)6.25 * height) - (5 * age) - 161);
            }


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

            int Carbohydrates = (int)(0.40 * BMR) / 4;
            int Proteins = (int)(0.30 * BMR) / 4;
            int Fats = (int)(0.30 * BMR) / 9;

            var existingDailyMacros = _context.DailyMacros.FirstOrDefault(dm => dm.BodyId == currentBody.BodyID);

            if (existingDailyMacros != null)
            {
                // Update existing record
                existingDailyMacros.CaloriesRecommended = (int)BMR;
                existingDailyMacros.CarbohydratesRecommended = Carbohydrates;
                existingDailyMacros.ProteinsRecommended = Proteins;
                existingDailyMacros.FatsRecommended = Fats;

                _context.Update(existingDailyMacros);
            }
            else
            {
                var DailyMacros = new DailyMacros
                {
                    BodyId = currentBody.BodyID,
                    CaloriesRecommended = (int)BMR,
                    CaloriesConsumed = 0,
                    CarbohydratesRecommended = Carbohydrates,
                    CarbohydratesConsumed = 0,
                    ProteinsRecommended = Proteins,
                    ProteinsConsumed = 0,
                    FatsRecommended = Fats,
                    FatsConsumed = 0
                };

                await _context.AddAsync(DailyMacros);
            }
            
            await _context.SaveChangesAsync();
        }

        public DailyMacros GetMacros<DailyMacros>(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDailyMacros(UpdateMacrosInputModel input, string userId)
        {
            var today = DateTime.Today;

            var existingBodyInfo = _context.Bodies.Where(x => x.UserID == userId && x.EffectiveThroughDate == DateTime.MaxValue)
                .OrderByDescending(x => x.EffectiveFromDate)
                .FirstOrDefault();

            var existingEntry = _context.DailyMacros.FirstOrDefault(x => x.BodyId == existingBodyInfo.BodyID);


           throw new NotImplementedException();

        }
    }
}
