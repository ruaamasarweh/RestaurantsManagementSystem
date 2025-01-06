using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class Favorite
    {
        [Key]
        public int FavoriteID { get; set; }


        public int UserID {  get; set; }
        public User? User { get; set; }


        public int BranchID { get; set; }
        public Branch? Branch { get; set; }
    }
}
