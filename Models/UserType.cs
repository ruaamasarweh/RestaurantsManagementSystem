using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class UserType
    {
        [Key]
        public int UserTypeID { get; set; }

        
        public string? TypeName { get; set; }


        public ICollection<User>? Users { get; set; }
    }
}
