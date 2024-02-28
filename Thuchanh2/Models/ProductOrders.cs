using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thuchanh2.Models
{
    public class ProductOrders
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Product Id is required")]
        [ForeignKey("Product")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Order Id is required")]
        [ForeignKey("Order")]
        [Display(Name = "Order")]
        public int OrderId { get; set; }

        // Navigation properties
        public virtual Products Product { get; set; }
        public virtual Orders Order { get; set; }
    }
}
