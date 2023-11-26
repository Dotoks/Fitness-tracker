using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.ViewModels.Calories
{
    public class UpdateMacrosInputModel
    {
        [Required]
        public int CaloriesRecommended { get; set; }

        public int CaloriesConsumed { get; set; } = 0;

        [Required]
        public int CarbohydratesRecommended { get; set; } = 0;

        public int CarbohydratesConsumed { get; set; } = 0;

        [Required]
        public int ProteinsRecommended { get; set; }

        public int ProteinsConsumed { get; set; } = 0;

        [Required]
        public int FatsRecommended { get; set; }

        public int FatsConsumed { get; set; } = 0;

    }
}
