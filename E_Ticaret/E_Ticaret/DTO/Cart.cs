using E_Ticaret.DTO;
using System.Text.Json.Serialization;
namespace E_Ticaret.DTO
{
    public class Cart
    {
        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }

        [JsonPropertyName("ctotal")]
        public double CTotal { get; set; }

        [JsonPropertyName("carts")]
        public List<Carts> Carts { get; set; } = new List<Carts>()!;

        [JsonPropertyName("coupon")]
        public CouponHistory Coupon { get; set; } = new CouponHistory()!;
    }
}