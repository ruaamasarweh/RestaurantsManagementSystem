using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }


        [Required]
        public int NumOfPeople { get; set; }


        [Required]
        public DateTime Date { get; set; }


        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public string? TableZone { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }


        public int ReservationStatusID { get; set; }
        public ReservationStatus? ReservationStatus { get; set; }


        public int BranchID { get; set; }
        public Branch? Branch { get; set; }

        public ICollection<ReservationDish>? ReservationDishes { get; set; }
    }
}

