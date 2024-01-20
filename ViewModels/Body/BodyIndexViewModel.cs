namespace Fitness_Tracker.ViewModels.Body
{
    public class BodyIndexViewModel
    {
        public Models.Body UserBody { get; set; }
        public int TotalCaloriesConsumed { get; set; }
        public int TotalCaloriesRecommended { get; set; }

        // Include properties for proteins, fats, and carbohydrates
        public int TotalProteinsConsumed { get; set; }
        public int TotalFatsConsumed { get; set; }
        public int TotalCarbohydratesConsumed { get; set; }

        public int TotalProteinsRecommended { get; set; }
        public int TotalFatsRecommended { get; set; }
        public int TotalCarbohydratesRecommended { get; set; }
    }

}
