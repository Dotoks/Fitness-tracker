using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class UserScraped
    {
        [Key]
        public long UserScrapedID { get; set; }

        [Required]
        public string Name { get; set; }

        // Add any other properties you need for scraped users, such as scraped email and password if available

        public List<RecipeScraped> CreatedRecipes { get; set; }
    }
}
