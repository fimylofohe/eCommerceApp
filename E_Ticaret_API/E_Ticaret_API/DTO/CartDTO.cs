namespace E_Ticaret_API.DTO
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public string? GuestToken { get; set; }
        public int? UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double? Total { get; set; }
        public DateTime AddDate { get; set; }

        public ProductDTO Product { get; set; } = null!;
        public UserDTO User { get; set; } = null!;
    }
}
