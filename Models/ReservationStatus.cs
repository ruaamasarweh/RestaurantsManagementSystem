using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    [Index(nameof(ReservationStatus.ReservationnStatus), IsUnique = true)]
    public class ReservationStatus
    {
        [Key]
        public int ReservationStatusID { get; set; }


        [Required]
        [StringLength(20)]
        public string? ReservationnStatus { get; set; }


        public ICollection<Reservation>? Reservations { get; set; }
    }
}
