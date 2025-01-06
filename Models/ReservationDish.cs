using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class ReservationDish
    {
        [Key]
        public int ID_Reservation_Dish { get; set; }


        public int ID_Reservation { get; set; }
        public Reservation? Reservation { get; set; }


        public int ID_Dish { get; set; }
        public Dish? Dish { get; set; }


        public ICollection<Order>? Orders { get; set; }
    }
}
