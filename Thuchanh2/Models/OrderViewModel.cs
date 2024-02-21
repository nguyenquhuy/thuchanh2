using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Thuchanh2.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [DisplayName("Order Date")]
        public DateTime? OrderDate { get; set; }

        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [DisplayName("Customer Name")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
