using E_Ticaret.DTO;
using System.Text.Json.Serialization;
namespace E_Ticaret.DTO
{
    public class Carts
    {
        [JsonPropertyName("cartId")]
        public int CartId { get; set; }

        [JsonPropertyName("guestToken")]
        public string? GuestToken { get; set; }

        [JsonPropertyName("userId")]
        public int? UserId { get; set; }

        [JsonPropertyName("productId")]
        public int? ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("total")]
        public double? Total { get; set; }

        [JsonPropertyName("addDate")]
        public DateTime AddDate { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; } = null!;

        [JsonPropertyName("product")]
        public Product Product { get; set; } = null!;
    }
}