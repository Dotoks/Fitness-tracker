using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Models
{
    public class User : IdentityUser
    {
        [Required]
        public int Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; } // Hashed for security
        // Other user-related fields as needed                                 
        public List<Recipe>? CreatedRecipes { get; set; }

        public List<Body>? BodyMeasurements { get; set; }
    }
}
