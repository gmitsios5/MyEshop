using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEshop.Models
{
    [Serializable]
    [Table("Products")]
    public class Products
    {
        public Products()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
        }

            [Key]
            [Display(Name = "ProductID")]
            public int ProductID { get; set; }

            [Display(Name = "Image")]
            [Column(TypeName = "nvarchar(255)")]
            public string? ProductImage { get; set; }

            [Display(Name = "Product Title")]
            [Column(TypeName = "nvarchar(255)")]
            public string? ProductTitle { get; set; }

            [Display(Name = "Description")]
            [Column(TypeName = "nvarchar(max)")]
            public string? Description { get; set; }

            [Display(Name = "Value")]
            [Column(TypeName = "decimal(4,2)")]
            
            public decimal? Value { get; set; }

            [Display(Name = "Quantity")]
            [Column(TypeName = "int")]

            public int? Quantity { get; set; }

            public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
