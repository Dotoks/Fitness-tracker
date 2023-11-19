using Fitness_Tracker.Services;
using Fitness_Tracker.ViewModels.Body;
using Fitness_Tracker.ViewModels.Recipes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers
{
    public class BodyController : Controller
    {
        private readonly IBodyService bodiesService;
        private readonly ICaloriesService caloriesService;
        private readonly UserManager<IdentityUser> userManager;
        public BodyController(IBodyService bodyService,ICaloriesService caloriesService, UserManager<IdentityUser> userManager)
        {
            this.bodiesService = bodyService;
            this.caloriesService = caloriesService;
            this.userManager = userManager;
        }
        // GET: BodyController
        public ActionResult Index()
        {
            return View();
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
        public async Task<ActionResult> CreateAsync(CreateBodyInputModel input)
        {
            var user = await userManager.GetUserAsync(this.User);

            try
            {
                await bodiesService.CreateAsync(input, user.Id);
                await caloriesService.CreateAsync(user.Id, input.Weight, input.Height, input.Age, input.ActivityLevel);
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
