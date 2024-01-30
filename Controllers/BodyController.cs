using Fitness_Tracker.Models;
using Fitness_Tracker.Services;
using Fitness_Tracker.ViewModels.Body;
using Fitness_Tracker.ViewModels.Recipes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers
{
    public class BodyController : Controller
    {
        private readonly IBodyService bodiesService;
        private readonly IMacrosService macrosService;
        private readonly UserManager<IdentityUser> userManager;
        public BodyController(IBodyService bodyService,IMacrosService macrosService, UserManager<IdentityUser> userManager)
        {
            this.bodiesService = bodyService;
            this.macrosService = macrosService;
            this.userManager = userManager;
        }
        // GET: BodyController
        public ActionResult Index()
        {
            var userId = userManager.GetUserId(this.User);
            Body userBody = bodiesService.GetBody(userId);

            if (userBody != null && userBody.DailyMacros != null)
            {
                // Calculate total macros consumed and recommended
                int totalCaloriesConsumed = userBody.DailyMacros.CaloriesConsumed;
                int totalCaloriesRecommended = userBody.DailyMacros.CaloriesRecommended;

                int totalProteinsConsumed = userBody.DailyMacros.ProteinsConsumed;
                int totalFatsConsumed = userBody.DailyMacros.FatsConsumed;
                int totalCarbohydratesConsumed = userBody.DailyMacros.CarbohydratesConsumed;

                int totalProteinsRecommended = userBody.DailyMacros.ProteinsRecommended;
                int totalFatsRecommended = userBody.DailyMacros.FatsRecommended;
                int totalCarbohydratesRecommended = userBody.DailyMacros.CarbohydratesRecommended;

                // Create an instance of BodyIndexViewModel
                var viewModel = new BodyIndexViewModel
                {
                    UserBody = userBody,
                    TotalCaloriesConsumed = totalCaloriesConsumed,
                    TotalCaloriesRecommended = totalCaloriesRecommended,
                    TotalProteinsConsumed = totalProteinsConsumed,
                    TotalFatsConsumed = totalFatsConsumed,
                    TotalCarbohydratesConsumed = totalCarbohydratesConsumed,
                    TotalProteinsRecommended = totalProteinsRecommended,
                    TotalFatsRecommended = totalFatsRecommended,
                    TotalCarbohydratesRecommended = totalCarbohydratesRecommended
                    // Add other properties as needed
                };

                return View(viewModel);
            }
            else
            {
                // Handle the case where userBody or userBody.DailyMacros is null
                return View("ErrorMacros"); // You can customize the error view
            }
        }

        // GET: BodyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BodyController/Create
        public ActionResult Create()
        {
            var viewModel = new CreateBodyInputModel();

            return this.View(viewModel);
        }

        // POST: BodyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> CreateAsync(CreateBodyInputModel input)
        {
            var user = await userManager.GetUserAsync(this.User);

            try
            {
                await bodiesService.CreateAsync(input, user.Id);
                await macrosService.CreateAsync(user.Id, input.Weight, input.Height, input.Age, input.ActivityLevel, input.Gender);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return View(input); 
            }

            return this.Redirect("/");
        }

        // GET: BodyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BodyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BodyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BodyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
