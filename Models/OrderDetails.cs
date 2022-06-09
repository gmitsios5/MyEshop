using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyEshop.Models
    {
        [Table("OrderDetails")]
        public class OrderDetails
        {
        [Key]
        [Display(Name = "OrderDetailsId")]
        public int? OrderDetailsId { get; set; }

        [Display(Name = "Order")]
        public int? OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Orders? Order { get; set; }

        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products? Products { get; set; }

        [Display(Name = "Value")]
        [Column(TypeName = "decimal(4,2)")]
        public decimal? Value { get; set; }

        [Display(Name = "Quantity")]
        [Column(TypeName = "int")]
        public int? Quantity { get; set; }



    }
    }

