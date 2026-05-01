// DataAnnotations 描述「這個欄位有什麼規則」，進行資料驗證，也能影響資料表欄位的設定
using System.ComponentModel.DataAnnotations;



// string? 允許空值


namespace EmployeeAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = "";


        [StringLength(50)]
        public string? Department { get; set; }


        [StringLength(20)]
        public string? Telephone { get; set; }


        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }
    }
}
