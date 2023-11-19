using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.ViewModels.Body
{
    public class CreateBodyInputModel
    {
        [Required]
        public decimal Weight { get; set; }
        
        [Required]
        public decimal Height { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string ActivityLevel { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveThroughDate { get; set; }

        public bool CurrentRecordIndicator { get; set; }
    }
}
