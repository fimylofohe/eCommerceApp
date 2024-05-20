using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public string? GuestToken { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public DateTime AddDate { get; set; }
    }
}
