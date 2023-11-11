using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers
{
    public class CaloriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateDailyCalories(int userId)
        {
            var today = DateTime.Today;

            return View();
        }
    }
}
