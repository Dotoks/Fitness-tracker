using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Tracker.Models
{
    public class DailyCalories
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("BodyId")]
        public Body Body { get; set; }

        public int BodyId { get; set; }

        public int CaloriesConsumed { get; set; } = 0;

        public int CaloriesRecommended { get; set; }
    }
}
