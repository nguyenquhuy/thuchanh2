using System.ComponentModel;

namespace Thuchanh2.Models
{
    public class ProductOrderViewModel
    {
        public int Id { get; set; }
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Product Name")]
        public string? Name { get; set; }

        public int orderId { get; set; }
    }
}
