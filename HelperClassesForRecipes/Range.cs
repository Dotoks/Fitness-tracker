using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.HelperClassesForRecipes
{
    public class TimeRange
    {
        [Range(0, 23, ErrorMessage = "Hours must be between 0 and 23.")]
        public int MinHours { get; set; }

        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59.")]
        public int MinMinutes { get; set; }

        [Range(0, 23, ErrorMessage = "Hours must be between 0 and 23.")]
        public int MaxHours { get; set; }

        [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59.")]
        public int MaxMinutes { get; set; }
    }
}
