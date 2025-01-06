using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public required string CustomerReview {  get; set; }

        [Required]
        public int Rating {  get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }


        public int BranchID {  get; set; }
        public Branch? Branch { get; set; }
        
    }
}
