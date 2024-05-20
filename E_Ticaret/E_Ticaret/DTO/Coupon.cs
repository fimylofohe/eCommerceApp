using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Coupon
    {
        [JsonPropertyName("couponId")]
        public int CouponId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("discountAmount")]
        public int? DiscountAmount { get; set; }

        [JsonPropertyName("couponCode")]
        public string? CouponCode { get; set; }

        [JsonPropertyName("validityDate")]
        public DateTime? ValidityDate { get; set; }

        [JsonPropertyName("singleUse")]
        public bool SingleUse { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
