using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;
using Fitness_Tracker.Data;
using Fitness_Tracker.HelperClassesForScraping;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fitness_Tracker.HelperClassesForRecipes;

namespace Fitness_Tracker.Controllers;

public class RecipesScraperController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<IdentityUser> userManager;
   

    public RecipesScraperController(IUnitOfWork unitOfWork, IRecipeRepository recipeRepository, UserManager<IdentityUser> userManager, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _recipeRepository = recipeRepository;
        this.userManager = userManager;
        _userRepository = userRepository;
    }


    //This is not fully implemented, in order for it to work correctly, you should use AJAX 
    //Use JavaScript to update the content on the page dynamically when the AJAX response is received.
    //Replace the existing list of recipes with the updated list based on the user's filter criteria.
    [HttpGet]
    public async Task<IActionResult> Index(List<string>? ingredientFilter, TimeRange? cookingTimeFilter, string? recipeNameFilter)
    {
        try
        {
            await ScrapeData();
            IEnumerable<Recipe> recipes;
            recipes = _recipeRepository.FilterByIngredient(ingredientFilter, cookingTimeFilter, recipeNameFilter);

            return View(recipes);
        }
        catch (Exception ex)
        {
          
            return View("Error"); 
        }
    }








    public async Task ScrapeData()
    {

        string filePath = "MealLinks.txt";
        ScrapeAndSaveMealLinks(filePath); // scrapes the links for the meals
        string filePathEverythingscrapedTxt = "EveryThingScraped.txt";
        List<string> mealLinks = new List<string>();
        // Accumulate scraped instructions in a list
        List<string> allScrapedInstructions = new List<string>();
        if (System.IO.File.Exists(filePath))
        {
            mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
        }
        if (!System.IO.File.Exists(filePathEverythingscrapedTxt))
        {
            foreach (var link in mealLinks)
            {

                //This part scrapes the name of the meal!!!
                var scrapedName = await ScrapeNameAsync(link);

                //This part scrapes the  user 
                var scrapeAndCreateFakeUserAccount = await ScrapeUserAsync(link);

                //This part scrapes the creation date
                var scrapedCreation = await ScrapeCreationDateAsync(link);

                //This part scrapes the description of a recipe 
                var scrapedDesc = await ScrapeDescriptionAsync(link);

                //This part scrapes the cooking time
                var scrapedCookingTime = await ScrapeCookingTimeAsync(link);

                //This part scrapes the servings
                var scrapedServings = await ScrapeServingsAsync(link);
                

                //This part scrapes the calories
                var scrapedCalories = await ScrapeCaloriesAsync(link);

                //This part scrapes the fats
                var scrapedFats = await ScrapeFatsAsync(link);

                //This part scrapes the carbs
                var scrapedCarbs = await ScrapeCarbsAsync(link);

                //This part scrapes the protein
                var scrapedProtein = await ScrapeProteinAsync(link);






                



                


                Recipe recipe = new Recipe
                {
                    RecipeName = scrapedName,
                    CookingTime = scrapedCookingTime,
                    CreatedDate = scrapedCreation,
                    Description = scrapedDesc,
                    Servings = scrapedServings,
                    DifficultyLevel = null,
                    CreatedBy = scrapeAndCreateFakeUserAccount.Id,
                    Creator = scrapeAndCreateFakeUserAccount as User
                   // PreparationInstructions = scrapedInstructions
                    // Macros = new List<Macro>(),
                };
                // Add the recipe to the database context
                _unitOfWork.Recipe.Add(recipe);
                _unitOfWork.Save();




                //this scrapes  ingredients
                var scrapedIngredients = await ScrapeIngredientsAsync(link);


                var ingredientIds = new List<int>();
                foreach (var ingredientData in scrapedIngredients)
                {
                    var ingredient = new Ingredient
                    {
                        Quantity = ingredientData.Quantity,
                        IngredientName = ingredientData.IngredientName,
                        Unit = ingredientData.Unit
                    };

                    // Add the ingredient to the database context and save changes to get IngredientID
                    _unitOfWork.Ingredient.Add(ingredient);
                    _unitOfWork.Save();

                    ingredientIds.Add(ingredient.IngredientID);
                }

                foreach (var ingredientId in ingredientIds)
                {
                    var macro = new Macro
                    {
                        RecipeID = recipe.RecipeID,
                        IngredientID = ingredientId,
                        Calories = scrapedCalories,//... get the value from scraping,
                        Fats =scrapedFats, //... get the value from scraping,
                        Carbohydrates = scrapedCarbs,//... get the value from scraping,
                        Proteins =scrapedProtein //... get the value from scraping,
                    };

                    // Add the Macro to the database context and save changes
                    _unitOfWork.Macro.Add(macro);
                   _unitOfWork.Save();
                }


                //This part scrapes the instructions 
                var scrapedInstructions = await ScrapeInstructionsAsync(link);

                foreach (var instructionText in scrapedInstructions)
                {
                    var instruction = new Instruction
                    {
                        InstructionName = instructionText.InstructionName,
                        RecipeId = recipe.RecipeID
                    };

                    // Add the instruction to the database context and save changes
                    _unitOfWork.Instruction.Add(instruction);
                    _unitOfWork.Save();
                }
               
            }
        }

        if (!System.IO.File.Exists(filePathEverythingscrapedTxt))
        {
            System.IO.File.Create(filePathEverythingscrapedTxt);
        }
        
        
        
    }







    //Helper methods for scraping
    private async Task<decimal> ScrapeProteinAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);
        string ProteinXPath = "//*[@id=\"mntl-nutrition-facts-summary_1-0\"]/table/tbody/tr[4]/td[1]";
        var ProteinNode = doc.DocumentNode.SelectSingleNode(ProteinXPath);
        if (ProteinNode != null)
        {
            string ProteinText = ProteinNode.InnerText.Trim();

            // Extract the numeric part using a regular expression
            string numericValue = Regex.Match(ProteinText, @"\d+").Value;

            if (decimal.TryParse(numericValue, out decimal ProteinValue))
            {
                return ProteinValue;
            }
        }

        return 0; // Return a default value if scraping fails or no numeric value found
    }
    private async Task<decimal> ScrapeCarbsAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);
        string CarbsXPath = "//*[@id=\"mntl-nutrition-facts-summary_1-0\"]/table/tbody/tr[3]/td[1]";
        var CarbsNode = doc.DocumentNode.SelectSingleNode(CarbsXPath);
        if (CarbsNode != null)
        {
            string CarbsText = CarbsNode.InnerText.Trim();

            // Extract the numeric part using a regular expression
            string numericValue = Regex.Match(CarbsText, @"\d+").Value;

            if (decimal.TryParse(numericValue, out decimal CarbsValue))
            {
                return CarbsValue;
            }
        }

        return 0; // Return a default value if scraping fails or no numeric value found
    }
    private async Task<decimal> ScrapeFatsAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);
        string fatsXPath = "//*[@id=\"mntl-nutrition-facts-summary_1-0\"]/table/tbody/tr[2]/td[1]";
        var fatsNode = doc.DocumentNode.SelectSingleNode(fatsXPath);
        if (fatsNode != null)
        {
            string fatsText = fatsNode.InnerText.Trim();

            // Extract the numeric part using a regular expression
            string numericValue = Regex.Match(fatsText, @"\d+").Value;

            if (decimal.TryParse(numericValue, out decimal fatsValue))
            {
                return fatsValue;
            }
        }

        return 0; // Return a default value if scraping fails or no numeric value found
    }
    private async Task<decimal> ScrapeCaloriesAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);
        string caloriesXPath = "//*[@id=\"mntl-nutrition-facts-summary_1-0\"]/table/tbody/tr[1]/td[1]";
        var calorieNode = doc.DocumentNode.SelectSingleNode(caloriesXPath);
        if (calorieNode != null)
        {
            string calorieText = calorieNode.InnerText.Trim();
            if (decimal.TryParse(calorieText, out decimal calorieValue))
            {
                return calorieValue;
            }
            else
            {
                return 0; 
            }
        }
        else
        {
            return 0;
        }
    }
    private async Task<IdentityUser> CreateUserAsync(string name, string email, string password)
    {
        // Filter out non-alphanumeric characters from the name
        var cleanedName = new string(name.Where(char.IsLetterOrDigit).ToArray());
        var cleanedEmail = $"{cleanedName}@gmail.com";
        var existingUser = await userManager.FindByNameAsync(cleanedName);

        if (existingUser != null)
        {
           
            //var user =   _unitOfWork.User.Get(u => u.Email == email);
            return existingUser;
        }
        else
        {
            
            
            var user = new User { Name = cleanedName, UserName = cleanedEmail, Email = cleanedEmail };
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                // Handle the case where user creation failed
                throw new InvalidOperationException($"User creation failed for {name}. Reason: {string.Join(", ", result.Errors)}");
            }
            return user;
        }
    }




    private async Task<IdentityUser> ScrapeUserAsync(string url)//Scrapes the userName. Create fake accounts!!!
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);
        string userXPath = "//*[@id=\"mntl-bylines__item_1-0\"]/a";
        var nameOfUserNode = doc.DocumentNode.SelectSingleNode(userXPath);

        if (nameOfUserNode != null)
        {
            // Get the full name
            string fullName = nameOfUserNode.InnerText.Trim();

            // Extract the first name
            string firstName = fullName.Split(' ')[0];
            string email = $"{firstName}@gmail.com";
            string password = "Qqq123*";
            var user = await userManager.FindByNameAsync(email);//_unitOfWork.User.Get(u => u.UserName == email);
            // Create the user with the scraped information
            if (user == null)
            {
                return await CreateUserAsync(firstName, email, password);
            }
            else
            {
              //  _userRepository.Detach(user);
                return user;
            }
        }
        else
        {
           
            // Create a default user if name not found
            string defaultName = "Unknown";
            string defaultEmail = "unknown@gmail.com";
            string defaultPassword = "Qqq123*";
            var userUnknown = await userManager.FindByNameAsync(defaultEmail);
            if (userUnknown == null)
            {
                // Create a user with default information

                return await CreateUserAsync(defaultName, defaultEmail, defaultPassword);
            }
            else
            {
                return userUnknown;

            }
          
        }
    }

    private async Task<DateTime> ScrapeCreationDateAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);
        string creationXPath = "//*[@id=\"mntl-bylines__group_1-0\"]/div[2]";
        var creation = doc.DocumentNode.SelectSingleNode(creationXPath);
        if (creation != null)
        {
            // Use a regular expression to extract the date in the "Month Day, Year" format
            string creationText = creation.InnerText.Trim();
            string datePattern = @"\b(?:January|February|March|April|May|June|July|August|September|October|November|December)\s+\d{1,2},\s+\d{4}\b";
            Match match = Regex.Match(creationText, datePattern);

            if (match.Success)
            {
                // Parse the matched date into a DateTime
                if (DateTime.TryParse(match.Value, out DateTime creationDate))
                {
                    return creationDate;
                }
            }
        }

        // If the date is not found or cannot be parsed, return DateTime.MinValue or another suitable default value.
        return DateTime.MinValue;
    }
    private async Task<int> ScrapeServingsAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);


        var labelNode = doc.DocumentNode.SelectSingleNode("//div[@class='mntl-recipe-details__label'][contains(text(), 'Servings')]");
        if (labelNode != null)
        {
            // Get the corresponding "mntl-recipe-details__value" within the same parent div
            var valueNode = labelNode.SelectSingleNode("../div[@class='mntl-recipe-details__value']");

            if (int.TryParse(valueNode.InnerText.Trim(), out int servings))
            {
                return servings;
            }
        }

        return -1;
    }

    private async Task<string> ScrapeCookingTimeAsync(string url)//check if correct
    {//Warning : maybe you should use Descendants to go into the div to scrape the cooking time
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);

        // Look for the div element with the text "Total Time"
        var labelNode = doc.DocumentNode.SelectSingleNode("//div[@class='mntl-recipe-details__label'][contains(text(), 'Total Time')]");

        if (labelNode != null)
        {
            // Get the corresponding "mntl-recipe-details__value" within the same parent div
            var valueNode = labelNode.SelectSingleNode("../div[@class='mntl-recipe-details__value']");

            if (valueNode != null)
            {
                return valueNode.InnerText.Trim();
            }
        }

        return "Total Time not found";

    }
    private async Task<string> ScrapeNameAsync(string url)//Name of meal
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);
        string nameXPath = "//*[@id=\"article-heading_1-0\"]";
        var nameNode = doc.DocumentNode.SelectSingleNode(nameXPath);

        if (nameNode != null)
        {
            
            return nameNode.InnerText.Trim(); 
        }
        else
        {
            return "Name not found"; 
        }
    }
    private async Task<string> ScrapeDescriptionAsync(string url)//check if correct
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);
        string descriptionXPath = "//*[@id=\"article-subheading_1-0\"]";
        var description = doc.DocumentNode.SelectSingleNode(descriptionXPath);
        if (description != null)
        {
            return description.InnerText.Trim();
        }
        return "Name not found";
        
    }


    private async Task<List<Instruction>> ScrapeInstructionsAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);

        string instructionsXPath = "//*[@id=\"mntl-sc-block_2-0\"]";//ol element for steps

        var instructionList = doc.DocumentNode.SelectSingleNode(instructionsXPath);
        List<Instruction> steps = new List<Instruction>();
        if (instructionList != null)
        {
            var instructionNodes = instructionList.SelectNodes("li");

            if (instructionNodes != null)
            {
                foreach (var instructionNode in instructionNodes)
                {
                    // Extract only the <p> elements with a specific class (e.g., "comp mntl-sc-block mntl-sc-block-html")
                    var pElements = instructionNode.Descendants("p")
                        .Where(p => p.GetAttributeValue("class", "").Contains("comp mntl-sc-block mntl-sc-block-html"))
                        .ToList();

                    foreach (var pElement in pElements)
                    {
                        string instructionText = pElement.InnerText.Trim();
                        Instruction instruction = new Instruction { InstructionName = instructionText };
                      // _unitOfWork.Instruction.Add(instruction);
                        steps.Add(instruction);
                    }
                }

              //  await _unitOfWork.SaveAsync();
            }
        }

        return steps;


    }

    private async Task<List<Ingredient>> ScrapeIngredientsAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);

        string ingredientsXPath = "//*[@id='mntl-structured-ingredients_1-0']/ul";
        HtmlNode ingredientList = doc.DocumentNode.SelectSingleNode(ingredientsXPath);
        List<Ingredient> ingredients = new List<Ingredient>();
        HashSet<string> uniqueIngredients = new HashSet<string>();

        if (ingredientList != null)
        {
            var ingredientNodes = ingredientList.SelectNodes("li");

            if (ingredientNodes != null)
            {
                foreach (var ingredientNode in ingredientNodes)
                {
                    // Extract the ingredient text and add it to your model.
                   // string ingredientText = ingredientNode.InnerText.Trim();
                   // ingredients.Add(ingredientText);
                    string[] parts = ingredientNode.Descendants("span")
                        .Select(span => span.InnerText.Trim())
                        .ToArray();
                    if (parts.Length < 2)
                    {
                        continue; // Skip this ingredient and move to the next one.
                    }
                    string quantity = parts[0];
                    string unit = parts[1];
                    string ingredientName = string.Join(" ", parts.Skip(2));

                    // Check if the ingredient name is unique
                    if (uniqueIngredients.Add(ingredientName))
                    {
                        // Create and add the Ingredient entity to the database
                        Ingredient ingredient = new Ingredient
                        {
                            Quantity = quantity,
                            Unit = unit,
                            IngredientName = ingredientName
                        };
                        ingredients.Add(ingredient);
                        //_unitOfWork.Ingredient.Add(ingredient);
                    }
                }

                // Save all unique ingredients to the database in a single transaction
               // await _unitOfWork.SaveAsync();
            }
        }

        return ingredients;
    }

    private void ScrapeAndSaveMealLinks(string filePath)
    {
        if (!System.IO.File.Exists(filePath) || new System.IO.FileInfo(filePath).Length == 0)
        {
            // Specify the URLs you want to scrape.
            string urlDinner = "https://www.allrecipes.com/recipes/17562/dinner/";
            string urlLunch = "https://www.allrecipes.com/recipes/17561/lunch/";
            string urlBreakfast = "https://www.allrecipes.com/recipes/78/breakfast-and-brunch/";
            string urlHealthy = "https://www.allrecipes.com/recipes/84/healthy-recipes/";
            string urlSalad = "https://www.allrecipes.com/recipes/96/salad/";
            string urlDesserts = "https://www.allrecipes.com/recipes/79/desserts/";
            string urlDrinks = "https://www.allrecipes.com/recipes/77/drinks/";

            // Create a WebClient to download the HTML content
            using (WebClient webClient = new WebClient())
            {
                string htmlDinner = webClient.DownloadString(urlDinner);
                string htmlLunch = webClient.DownloadString(urlLunch);
                string htmlBreakfast = webClient.DownloadString(urlBreakfast);
                string htmlHealthy = webClient.DownloadString(urlHealthy);
                string htmlSalads = webClient.DownloadString(urlSalad);
                string htmlDesserts = webClient.DownloadString(urlDesserts);
                string htmlDrinks = webClient.DownloadString(urlDrinks);

                // Load the HTML content into HtmlDocuments 
                HtmlDocument docDinner = new HtmlDocument();
                docDinner.LoadHtml(htmlDinner);

                HtmlDocument docLunch = new HtmlDocument();
                docLunch.LoadHtml(htmlLunch);

                HtmlDocument docBreakfast = new HtmlDocument();
                docBreakfast.LoadHtml(htmlBreakfast);

                HtmlDocument docHealthy = new HtmlDocument();
                docHealthy.LoadHtml(htmlHealthy);

                HtmlDocument docSalads = new HtmlDocument();
                docSalads.LoadHtml(htmlSalads);

                HtmlDocument docDesserts = new HtmlDocument();
                docDesserts.LoadHtml(htmlDesserts);

                HtmlDocument docDrinks = new HtmlDocument();
                docDrinks.LoadHtml(htmlDrinks);

                // Select all <a> elements that have the specified class and contain href attributes for dinner and lunch.
                var dinnerLinks =
                    docDinner.DocumentNode.SelectNodes("//a[contains(@class, 'mntl-card-list-items') and @data-doc-id]");
                var lunchLinks =
                    docLunch.DocumentNode.SelectNodes("//a[contains(@class, 'mntl-card-list-items') and @data-doc-id]");
                var breakfastLinks =
                    docBreakfast.DocumentNode.SelectNodes("//a[contains(@class, 'mntl-card-list-items') and @data-doc-id]");
                var HealthyLinks =
                    docHealthy.DocumentNode.SelectNodes("//a[contains(@class, 'mntl-card-list-items') and @data-doc-id]");
                var saladLinks =
                    docSalads.DocumentNode.SelectNodes("//a[contains(@class, 'mntl-card-list-items') and @data-doc-id]");
                var dessertLinks =
                    docDesserts.DocumentNode.SelectNodes("//a[contains(@class, 'mntl-card-list-items') and @data-doc-id]");
                var drinksLinks =
                    docDrinks.DocumentNode.SelectNodes("//a[contains(@class, 'mntl-card-list-items') and @data-doc-id]");

                if (dinnerLinks != null || lunchLinks != null || breakfastLinks != null || HealthyLinks != null ||
                    saladLinks != null || dessertLinks != null || drinksLinks != null)
                {
                    HashSet<string> mealHrefs = new HashSet<string>();

                    for (int i = 20; i < dinnerLinks.Count; i++)
                    {
                        string hrefDinner = dinnerLinks[i].GetAttributeValue("href", "");
                        mealHrefs.Add(hrefDinner);
                        if (i >= 55)
                        {
                            break;
                        }
                    }

                    for (int i = 20; i < lunchLinks.Count; i++)
                    {
                        string hrefLunch = lunchLinks[i].GetAttributeValue("href", "");
                        mealHrefs.Add(hrefLunch);
                        if (i >= 55)
                        {
                            break;
                        }
                    }

                    for (int i = 20; i < breakfastLinks.Count; i++)
                    {
                        string hrefBreakfast = breakfastLinks[i].GetAttributeValue("href", "");
                        mealHrefs.Add(hrefBreakfast);
                        if (i >= 55)
                        {
                            break;
                        }
                    }

                    for (int i = 20; i < HealthyLinks.Count; i++)
                    {
                        string hrefHealthy = HealthyLinks[i].GetAttributeValue("href", "");
                        mealHrefs.Add(hrefHealthy);
                        if (i >= 55)
                        {
                            break;
                        }
                    }

                    for (int i = 20; i < saladLinks.Count; i++)
                    {
                        string hrefSalads = saladLinks[i].GetAttributeValue("href", "");
                        mealHrefs.Add(hrefSalads);
                        if (i >= 55)
                        {
                            break;
                        }
                    }

                    for (int i = 20; i < dessertLinks.Count; i++)
                    {
                        string hrefDesserts = dessertLinks[i].GetAttributeValue("href", "");
                        mealHrefs.Add(hrefDesserts);
                        if (i >= 55)
                        {
                            break;
                        }
                    }

                    for (int i = 20; i < drinksLinks.Count; i++)
                    {
                        string hrefDrinks = drinksLinks[i].GetAttributeValue("href", "");
                        mealHrefs.Add(hrefDrinks);
                        if (i >= 55)
                        {
                            break;
                        }
                    }

                    // Write both dinner and lunch links to the same text file.
                    System.IO.File.WriteAllLines("MealLinks.txt", mealHrefs);
                }
            }
        }
        else
        {
            // Read the data from the file and set it in ViewData.
            var mealHrefsFromFile = System.IO.File.ReadAllLines(filePath).ToList();
            ViewData["ScrapedMealHrefs"] = mealHrefsFromFile;
        }
    }
}