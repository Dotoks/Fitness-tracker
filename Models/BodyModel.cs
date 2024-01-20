using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Tracker.Models
{
    public class Body
    {
        [Key]
        public int BodyID { get; set; } // Primary Key

        [Required]
        public string UserID { get; set; }
        [Required]
        [ForeignKey("UserID")] // This creates the Foreign Key for the User table
        public User User { get; set; }

        [Required]
        public DailyMacros DailyMacros { get; set; }

        // This table will be SCD 2
        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveThroughDate { get; set; }

        public bool CurrentRecordIndicator { get; set; }

        public required string ActivityLevel { get; set; }

        public string Gender { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public int Age { get; set; }

        public decimal BodyFatPercentage { get; set; }

    }
}
