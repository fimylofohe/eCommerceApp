﻿using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class CouponHistory
    {
        [JsonPropertyName("couponHistoryId")]
        public int CouponHistoryId { get; set; }

        [JsonPropertyName("couponId")]
        public int CouponId { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("coupon")]
        public Coupon Coupon { get; set; } = new Coupon()!;

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; } = new User()!;

        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
