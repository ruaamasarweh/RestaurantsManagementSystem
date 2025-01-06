using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class EmployeeType
    {
        [Key]
        public int EmployeeTypeID { get; set; }

        public string? EmpType { get; set; }


        public ICollection<Employee>? Employees { get; set; }
    }
}
