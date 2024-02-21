using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thuchanh2.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }        

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        // Navigation property
        public Customer? Customer { get; set; }
    }
}
