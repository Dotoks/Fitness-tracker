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
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers;

public class RecipesScraperController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRecipeRepository _recipeRepository;
    private static bool areIngredientsScraped = false;

    public RecipesScraperController(IUnitOfWork unitOfWork, IRecipeRepository recipeRepository)
    {
        _unitOfWork = unitOfWork;
        _recipeRepository = recipeRepository;
    }

    //TODO :scrape
    //public decimal Calories { get; set; }
    //public decimal Carbohydrates { get; set; }
    //public decimal Proteins { get; set; }
    //public decimal Fats { get; set; }




    [HttpGet]
    public async Task<IActionResult> ScrapeData()
    {
        string filePath = "MealLinks.txt";
        ScrapeAndSaveMealLinks(filePath); // scrapes the links for the meals
        string filePathIngredients = "IngredientsScraped.txt";
        List<string> mealLinks = new List<string>();
        // Accumulate scraped instructions in a list
        List<string> allScrapedInstructions = new List<string>();

        //This part scrapes the UserScraped model
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
              //  var scrapedNameOfUser = await ScrapeUserAsync(link);

              //   Console.WriteLine(scrapedNameOfUser);


                if (i > 2) break;

            }
        }

        //This part scrapes the name of the meal 
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
              //  var scrapedName = await ScrapeNameAsync(link);

              //  Console.WriteLine(scrapedName);


                if (i > 2) break;

            }
        }

        //This part scrapes the creation date
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
               // var scrapedCreation = await ScrapeCreationDateAsync(link);

                //Console.WriteLine(scrapedCreation.ToString("MM/dd/yyyy"));


                if (i > 2) break;

            }
        }

        //This part scrapes the description of a recipe 
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
              //  var scrapedDesc = await ScrapeDescriptionAsync(link);

              //  Console.WriteLine(scrapedDesc);


                if (i > 2) break;

            }
        }

        //This part scrapes the cooking time
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
              //  var scrapedCookingTime = await ScrapeCookingTimeAsync(link);

              //  Console.WriteLine(scrapedCookingTime);


                if (i > 2) break;

            }
        }

        //This part scrapes the servings
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
               // var scrapedServings = await ScrapeServingsAsync(link);

                 // Console.WriteLine(scrapedServings);


                if (i > 2) break;

            }
        }

        // This part of the code gets all the ingredient rows
        if (!System.IO.File.Exists(filePathIngredients))
        {
            
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }

            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
               // var scrapedInfo = await ScrapeIngredientsAsync(link);
                
            }
            System.IO.File.Create(filePathIngredients);
        }

        //This part scrapes the instructions 
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
               // var scrapedInstructions = await ScrapeInstructionsAsync(link);
              //  foreach (var scrapedInstruction in scrapedInstructions)
              //  {
                //    allScrapedInstructions.Add(scrapedInstruction); // Accumulate instructions
                    //TODO: seed database with the instructions (I have to create new recipe entities and then I will be able to do that
                    //)

              //  }
                if (i > 2) break;

            }
         // foreach (var item in allScrapedInstructions)
         // {
         //     await Console.Out.WriteLineAsync(item);
         // }
        }

        //This part scrapes the calories
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
              //  var scrapedCalories = await ScrapeCaloriesAsync(link);

                //  Console.WriteLine(scrapedCalories);


                if (i > 2) break;

            }
        }
        //This part scrapes the fats
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
               //  var scrapedFats = await ScrapeFatsAsync(link);

                //  Console.WriteLine(scrapedFats);


                if (i > 2) break;

            }
        }
        //This part scrapes the carbs
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
                var scrapedCarbs = await ScrapeCarbsAsync(link);

                Console.WriteLine(scrapedCarbs);


                if (i > 2) break;

            }
        }
        //This part scrapes the protein
        {
            if (System.IO.File.Exists(filePath))
            {
                mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
            }
            for (int i = 0; i < mealLinks.Count; i++)
            {
                string link = mealLinks[i];
                var scrapedProtein = await ScrapeProteinAsync(link);

                Console.WriteLine(scrapedProtein);


                if (i > 2) break;

            }
        }
        return View("ScrapeData");
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


    private async Task<string> ScrapeUserAsync(string url)//WARNING THIS IS UserScraped MODEL NOT User!!!!!!!!!!!!!!!!!!
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);
        string userXPath = "//*[@id=\"mntl-bylines__item_1-0\"]/a";
        var nameOfUserNode = doc.DocumentNode.SelectSingleNode(userXPath);
        if (nameOfUserNode != null)
        {

            return nameOfUserNode.InnerText.Trim();
        }
        return "Name not found";
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
    private async Task<string> ScrapeServingsAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);


        var labelNode = doc.DocumentNode.SelectSingleNode("//div[@class='mntl-recipe-details__label'][contains(text(), 'Servings')]");
        if (labelNode != null)
        {
            // Get the corresponding "mntl-recipe-details__value" within the same parent div
            var valueNode = labelNode.SelectSingleNode("../div[@class='mntl-recipe-details__value']");

            if (valueNode != null)
            {
                return valueNode.InnerText.Trim();
            }
        }

        return "Servings information not found";
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
    private async Task<string> ScrapeNameAsync(string url)
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


    private async Task<List<string>> ScrapeInstructionsAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);

        string instructionsXPath = "//*[@id=\"mntl-sc-block_2-0\"]";//ol element for steps

        var instructionList = doc.DocumentNode.SelectSingleNode(instructionsXPath);
        List<string> steps = new List<string>();
        if (instructionList != null)
        {
            var instructionNodes = instructionList.SelectNodes("li");//the mistake might be here

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
                        steps.Add(instructionText);
                    }
                }//almost ready


            }
        }

        return steps;


    }

    private async Task<List<string>> ScrapeIngredientsAsync(string url)
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);

        string ingredientsXPath = "//*[@id='mntl-structured-ingredients_1-0']/ul";
        HtmlNode ingredientList = doc.DocumentNode.SelectSingleNode(ingredientsXPath);
        List<string> ingredients = new List<string>();
        HashSet<string> uniqueIngredients = new HashSet<string>();

        if (ingredientList != null)
        {
            var ingredientNodes = ingredientList.SelectNodes("li");

            if (ingredientNodes != null)
            {
                foreach (var ingredientNode in ingredientNodes)
                {
                    // Extract the ingredient text and add it to your model.
                    string ingredientText = ingredientNode.InnerText.Trim();
                    ingredients.Add(ingredientText);
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

                        _unitOfWork.Ingredient.Add(ingredient);
                    }
                }

                // Save all unique ingredients to the database in a single transaction
                await _unitOfWork.SaveAsync();
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












//private async Task<List<string>> ScrapeInstructionsAsync(string url)
//{
//    HtmlWeb web = new HtmlWeb();
//    web.OverrideEncoding = Encoding.UTF8;
//    HtmlDocument doc = await web.LoadFromWebAsync(url);
//    var testList = new List<string>();
//    var stepsDiv = doc.DocumentNode.Descendants("div").FirstOrDefault(n => n.Id.Equals("recipe__steps_1-0"));
//    //check if null
//    if (stepsDiv == null)
//    {
//        throw new NullReferenceException("stepsDiv is null.");
//    }
//    // var str = "asd";
//    // testList.Add(str);

//    return testList;
//}