using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Tracker.Models
{
    public class Instruction//SCRAPED
    {
        public int id { get; set; }
        public string InstructionName { get; set; }


        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]

        public Recipe Recipe { get; set; }



       
    }
}
