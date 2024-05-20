using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string? AddressText {  get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
