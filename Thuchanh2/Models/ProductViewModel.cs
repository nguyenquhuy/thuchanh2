using System.ComponentModel;

namespace Thuchanh2.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [DisplayName("Product Name")]
        public string? Name { get; set; }


        [DisplayName("Product Price")]
        public int Price { get; set; }
    }
}
