using System.ComponentModel.DataAnnotations;
namespace Gp.Models
{
    public class ReservationDto
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }  
        public int NumOfPeople { get; set; }
        public string TableZone { get; set; }
        public int UserID { get; set; }
        public int BranchID { get; set; }
    }
}
