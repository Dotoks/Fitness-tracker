using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Tracker.Models
{
    public class DailyMacros
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("BodyId")]
        public Body Body { get; set; }

        public int BodyId { get; set; }

        public int CaloriesConsumed { get; set; } = 0;

        public int CaloriesRecommended { get; set; }

        public int CarbohydratesConsumed { get; set; } = 0;

        public int CarbohydratesRecommended { get; set; }

        public int ProteinsConsumed { get; set; } = 0;

        public int ProteinsRecommended { get; set; }

        public int FatsConsumed { get; set; } = 0;

        public int FatsRecommended { get; set; }
    }
}
