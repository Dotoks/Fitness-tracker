using System.Globalization;
using System.Net;
using System.Text;
using CsvHelper;
using Fitness_Tracker.Data;
using Fitness_Tracker.HelperClassesForScraping;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Controllers;

public class RecipesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRecipeRepository _recipeRepository;
    private static bool isDataScraped = false;
    public RecipesController(IUnitOfWork unitOfWork, IRecipeRepository recipeRepository)
    {
        _unitOfWork = unitOfWork;
        _recipeRepository = recipeRepository;
    }
    

    [HttpGet]
    public async Task<IActionResult> ScrapeData()
    {
     
        string filePath = "MealLinks.txt";
        List<string> scrapedData = new List<string>();
        ScrapeAndSaveMealLinks(filePath);//scrapes the links for the meals


        //this part of the code gets all the ingredient rows

        List<string> mealLinks = new List<string>();
        if (System.IO.File.Exists(filePath))
        {
            mealLinks = System.IO.File.ReadAllLines(filePath).ToList();
        }
        for (int i = 0; i < mealLinks.Count; i++)
        {
            string link = mealLinks[i];
            // Scrape data from the links using HtmlAgilityPack or any other HTML parsing library.
            var scrapedInfo = await ScrapeIngredientsAsync(link);

            foreach (var row in scrapedInfo)
            {
                scrapedData.Add(row);
            }
            scrapedData.Add("--------------------------------------------------");
            

            if (i >= 5)
            {
                break;
            }
            // Extract recipe name, description, ingredients, macros, etc.

            // Create instances of your model classes (Recipe, Ingredient, Macro) and populate their properties.

            // Save the scraped data to your database or data structures.
        }
        ViewData["ScrapedData"] = scrapedData;
        return View();
    }










    //Helper methods for scraping

    private async Task<List<string>> ScrapeIngredientsAsync(string url)//you pass every single link with recipe
    {
        HtmlWeb web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        HtmlDocument doc = await web.LoadFromWebAsync(url);


        // Define the XPath to select the <ul> element that contains the ingredients.
        string ingredientsXPath = "//*[@id='mntl-structured-ingredients_1-0']/ul";
        HtmlNode ingredientList = doc.DocumentNode.SelectSingleNode(ingredientsXPath);
        List<string> ingredients = new List<string>();


        if (ingredientList != null)
        {
            // Extract the individual <li> elements within the <ul> for ingredients.
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


                    string quantity = parts[0];
                    string unit = parts[1];
                    string ingredientName = string.Join(" ", parts.Skip(2));

                    // Create or update the Ingredient entity in the database
                    Ingredient ingredient = new Ingredient
                    {
                       Quantity = quantity,
                        Unit  = unit,
                        IngredientName = ingredientName
                    };
                   
                        _unitOfWork.Ingredient.Add(ingredient);
                       
                        await _unitOfWork.SaveAsync();
                    
                }

               
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






//[HttpGet]
//public IActionResult Index(List<Products> productsList)//be careful with using the real models. Maybe try using dto's!!!
// {//here you should have form that the user fills with products. The scraper scrapes all the recipes and outputs them under  the form .
        
        
        
//   return View();//View for main page in RecipesController
// }