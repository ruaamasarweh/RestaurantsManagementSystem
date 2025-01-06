using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class Signup
    {
        [Required]
        [StringLength(50)]
        public string? FullName { get; set; }


        [Required]
        [StringLength(30)]
        public string UserName { get; set; }


        [Required]
        public string PhoneNum { get; set; }


        [Required]
        [StringLength(30)]
        public string Address { get; set; }


        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        public string Password { get; set; }
    }
}
