namespace Gp.Models
{
    public class OrderDto
    {
        public int DishID { get; set; }
        public int ReservationID { get; set; }
        public int TableNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime Time { get; set; }
    }
}
