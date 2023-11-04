using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels.Recipes;
using Fitness_Tracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Fitness_Tracker.Repository.IRepository;
using Fitness_Tracker.Repository;

namespace Fitness_Tracker.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly UserManager<IdentityUser> userManager;
        public RecipesController(IRecipesService recipesServices, UserManager<IdentityUser> userManager)
        {
            this.recipesService = recipesServices;
            this.userManager = userManager;
        }
        // GET: RecipesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RecipesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecipesController/Create
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();

            return this.View(viewModel);
        }

        // POST: RecipesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(CreateRecipeInputModel input)
        {
            var user = await userManager.GetUserAsync(this.User);

            try
            {
                await recipesService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return View(input);
            }

            return this.Redirect("/");
        }

        // GET: RecipesController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecipesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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

        // GET: RecipesController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecipesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
