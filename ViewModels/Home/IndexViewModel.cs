using Fitness_Tracker.ViewModels.Body;
using Fitness_Tracker.ViewModels.Calories;

namespace Fitness_Tracker.ViewModels.Home
{
    public class IndexViewModel
    {
        public UpdateMacrosInputModel UpdateCaloriesInput { get; set; }

        public CreateBodyInputModel CreateBodyInput { get; set; }
    }
}
