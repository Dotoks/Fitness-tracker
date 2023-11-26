using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers
{
    public class MacrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateDailyMacros(int userId)
        {
            var today = DateTime.Today;

            return View();
        }
    }
}
