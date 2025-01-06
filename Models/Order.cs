using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }


        public int ID_Reservation_Dish { get; set; }
        public ReservationDish? ReservationDish { get; set; }


        public int TableNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime Time { get; set; }

        public bool IsTaken { get; set; }

    }
}