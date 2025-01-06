using System.ComponentModel.DataAnnotations;

namespace Gp.Models
{
    public class BranchDto
    {
        [Key]
        public int BranchID { get; set; }
        public string? LocationDescription { get; set; }
        public string? BranchImageUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public bool HasIndoorSeating { get; set; }
        public bool HasOutdoorSeating { get; set; }
        public int? NumOfTables { get; set; }
    }

}
