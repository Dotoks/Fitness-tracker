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

        public DateTime DateMeasurment { get; set; }

        public decimal Weight { get; set; }

        public decimal Height { get; set; }

        public decimal Kilograms { get; set; }

        public decimal BodyFatPercentage { get; set; }

    }
}
