using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gp.Models
{
    [Index(nameof(Employee.PhoneNum),IsUnique=true)]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }



        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Full name must be at least 6 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full name should not contain numbers or special characters.")]
        public string? FullName { get; set; }


        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Username must be at least 6 characters.")]
        [RegularExpression(@"^\S+$", ErrorMessage = "Username cannot contain spaces.")]
        public string? UserName { get; set; }


        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number.")]
        public string? PhoneNum { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string? Address { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }


        public int EmployeeTypeID { get; set; }
        public EmployeeType? EmployeeType { get; set; }



        public int BranchID { get; set; }
        public Branch? Branch { get; set; }
    }
}
