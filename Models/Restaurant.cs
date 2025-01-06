using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gp.Models
{
    [Index(nameof(Restaurant.RestaurantName),IsUnique =true)]   
    public class Restaurant
    {
        [Key]
        public int RestaurantID { get; set; }


        [Required]
        [StringLength(30)]
        public string? RestaurantName { get; set; }

        public string? ImageFilePath { get; set; }

        [NotMapped]
        [Required]
        public IFormFile? ImageFile { get; set; }

        public int UserID {  get; set; }
        public User? User { get; set; }


        public ICollection<Branch>? Branches { get; set; }

        public ICollection<DishCategory>? DishCategories { get; set; }
    }
}
