using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels.Recipes;
using Fitness_Tracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Fitness_Tracker.Repository.IRepository;
using Fitness_Tracker.Repository;
using Fitness_Tracker.HelperClassesForRecipes;
using X.PagedList;

namespace Fitness_Tracker.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly IRecipeRepository _recipeRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBodyRepository _bodyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public RecipesController(IRecipesService recipesServices, IUnitOfWork unitOfWork, IRecipeRepository recipeRepository, UserManager<IdentityUser> userManager, IBodyRepository bodyRepository, IUserRepository userRepository)
        {
            this.recipesService = recipesServices;
            this.userManager = userManager;
            _unitOfWork = unitOfWork;
            _bodyRepository = bodyRepository;
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
        }
        // GET: RecipesController
        [HttpGet]
        public async Task<IActionResult> Index(List<string>? ingredientsFilter, int? minHours, int? minMinutes, int? maxHours, int? maxMinutes, string? recipeNameFilter, int? caloriesMinFilter, int? caloriesMaxFilter, int? carbsFilter, int? proteinFilter, int? fatsFilter,
            int? page)
        {
            try
            {
                var scraperController = new RecipesScraperController(_unitOfWork, _recipeRepository, userManager, _userRepository);
               // await scraperController.ScrapeData();
                IEnumerable<Recipe> recipes;
                TimeRange? cookingTimeFilter = new TimeRange // Make Default values  00:00 and 23:59
                {
                    MinHours = minHours,
                    MinMinutes = minMinutes,
                    MaxHours = maxHours,
                    MaxMinutes = maxMinutes
                };

                recipes = _recipeRepository.Filter(ingredientsFilter, cookingTimeFilter, recipeNameFilter, caloriesMinFilter, caloriesMaxFilter, carbsFilter, proteinFilter, fatsFilter).ToList();



                // Implement pagination
                int itemsPerPage = 10; // Adjust the number of items per page as needed
                int currentPage = page ?? 1;

                return View(recipes.ToPagedList(currentPage, itemsPerPage));
            }
            catch (Exception ex)
            {

                return View("Error");
            }
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

        [HttpPost]
        public ActionResult AddToDailyMacros(int recipeId)
        {
            // Get the selected recipe based on the provided recipeId
            Recipe selectedRecipe = _recipeRepository.GetRecipeById(recipeId);
            var userId = userManager.GetUserId(this.User);

            // Get the user's body information (you need to implement this part based on your logic)
            Body userBody = _bodyRepository.GetUserBody(userId);

            // Update the daily macros based on the selected recipe
            UpdateDailyMacros(userBody.DailyMacros, selectedRecipe);

            // Save the changes to your data store or database
            _bodyRepository.SaveChanges();

            // Redirect or refresh the page as needed
            return RedirectToAction("Index");
        }

        private void UpdateDailyMacros(DailyMacros dailyMacros, Recipe recipeMacros)
        {
            // Update the daily macros
            dailyMacros.CaloriesConsumed += (int)recipeMacros.Calories;
            dailyMacros.ProteinsConsumed += (int)recipeMacros.Proteins;
            dailyMacros.CarbohydratesConsumed += (int)recipeMacros.Carbohydrates;
            dailyMacros.FatsConsumed += (int)recipeMacros.Fats;
            // Update other macro properties as needed
        }
    }
}
