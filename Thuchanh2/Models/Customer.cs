using System.ComponentModel.DataAnnotations;

namespace Thuchanh2.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Mobile { get; set; }
    }
}
