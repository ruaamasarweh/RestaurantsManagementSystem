using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class DishCategoryDto
    {
        [Key]
        public int DishCategoryID { get; set; }
        public string? Name { get; set; }
        public List<DishDto>? Dishes { get; set; }
    }

}
