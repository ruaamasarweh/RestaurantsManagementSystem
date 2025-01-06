using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class DishCategory
    {
        [Key]
        public int DishCategoryID { get; set; }


        [Required]
        [StringLength(50)]
        public string? Name { get; set; }


        public int RestaurantID { get; set; }
        public Restaurant? Restaurant { get; set; }


        public ICollection<Dish>? Dishes { get; set; }

    }
}
