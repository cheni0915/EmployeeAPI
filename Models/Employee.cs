using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }


        [StringLength(50)]
        public string Department { get; set; } = "";


        [StringLength(20)]
        public string Telephone { get; set; } = "";


        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = "";
    }
}
