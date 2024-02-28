using System.ComponentModel;

namespace Thuchanh2.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Phone")]
        public string? Phone { get; set; }
        [DisplayName("Email")]

        public string? Email { get; set; }
        [DisplayName("Mobile")]
        public string? Mobile { get; set; }

        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [DisplayName("Customer Name")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
