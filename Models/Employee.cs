using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEshop.Models
{
    [Table("Employee")]
    public class Employee
    {   [Key]
        [Display(Name ="ID")]
        public int Id { get; set; }

        [Display(Name = "Image")]
        [Column(TypeName = "nvarchar(255)")]
        public string? EmployeeImage { get; set; }

        [Display(Name ="Employee Full Name")]
        [Column(TypeName ="nvarchar(255)")]
        public string? EmployeeName { get; set; }

        [Display(Name ="Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MMMM-yyyy}")]
        public DateTime? HiringDate { get; set; }

    }
}
