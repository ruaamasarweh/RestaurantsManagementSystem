using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gp.Models
{
    public class Branch
    {
        [Key]
        public int BranchID { get; set; }

        [Required(ErrorMessage = "Location description is required.")]
        public string? LocationDescription { get; set; }

        [Required]
        [StringLength(12)]
        public string? phoneNumber { get; set; }

        [Required]
        public string? ImageFilePath { get; set; }

        [NotMapped]
        [Required]
        public IFormFile? ImageFile { get; set; }


        [Required]
        public TimeSpan? OpenTime { get; set; }

        [Required]
        public TimeSpan? CloseTime { get; set; }

        [Required]
        public bool HasIndoorSeating { get; set; }

        [Required]
        public bool HasOutdoorSeating { get; set; }

        [Required]
        public int? NumOfTables { get; set; }

        public int RestaurantID { get; set; }
        public Restaurant? Restaurant { get; set; }

        public ICollection<Employee>? Employees { get; set;}

        public ICollection<Reservation>? Reservations { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<Favorite>? Favorites { get; set; }
    }
}
