using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEshop.Models
{
    [Table("Orders")]
    public class Orders
    {

        public Orders()
        {
            this.OrderDetails = new List<OrderDetails>();
        }

        [Key]
        [Display(Name = "OrderID")]
        public int OrderID { get; set; }

        [Display(Name = "OrderStatus")]
        public string? OrderStatus { get; set; }

        [Display(Name = "CustomerName")]
        [Column(TypeName = "nvarchar(255)")]
        public string? CustomerName { get; set; }

        [Display(Name = "Email")]
        [Column(TypeName = "nvarchar(255)")]
        public string? Email { get; set; }


        [Display(Name = "Address")]
        [Column(TypeName = "nvarchar(255)")]
        public string? Address { get; set; }


        [Display(Name = "City")]
        [Column(TypeName = "nvarchar(255)")]
        public string? City { get; set; }

        [Display(Name = "StateCode")]
        public string? StateCode { get; set; }

        [Display(Name = "PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Notes")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }


        [Display(Name = "OrderDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime? OrderDate { get; set; }
        
        [Display(Name = "Products")]
        /*public List<Products>? Products { get; set; }*/
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
