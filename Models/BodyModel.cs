using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Tracker.Models
{
    public class Body
    {
        [Key]
        public int BodyID { get; set; } // Primary Key
        [ForeignKey("UserId")] // This creates the Foreign Key for the USer table

        // This table will be SCD 2
        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveThroughDate { get; set; }

        public bool CurrentRecordIndicator { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public decimal Kilograms { get; set; }

        public decimal BodyFatPercentage { get; set; }

    }
}
