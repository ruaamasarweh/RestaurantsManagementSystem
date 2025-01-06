using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gp.Models
{
    public class Dish
    {
        [Key]
        public int DishID { get; set; }

        [Required]
        [StringLength(40)]
        public string? DishName { get; set; }

        [Required]
        public string? ImageFilePath { get; set; }

        [NotMapped]
        [Required]
        public IFormFile? ImageFile { get; set; }

        public string? Details { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }



        public int DishCategoryID { get; set; }
        public DishCategory? DishCategory { get; set; }


        public ICollection<ReservationDish>? ReservationDishes { get; set; }

    }
}

