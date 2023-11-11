using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.ViewModels.Calories
{
    public class UpdateCaloriesInputModel
    {
        [Required]
        public int CaloriesConsumed { get; set; } = 0;

        public int CaloriesRecommended { get; set; }
    }
}
