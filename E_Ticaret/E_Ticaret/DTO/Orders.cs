using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Orders
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("addressId")]
        public int AddressId { get; set; }

        [JsonPropertyName("address")]
        public Addresses Address { get; set; } = new Addresses();

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; } = new User();

        [JsonPropertyName("orderKey")]
        public string? OrderKey { get; set; }

        [JsonPropertyName("amount")]
        public double? Amount { get; set; }

        [JsonPropertyName("couponAmount")]
        public double? CouponAmount { get; set; }

        [JsonPropertyName("totalAmount")]
        public double? TotalAmount { get; set; }

        [JsonPropertyName("couponHistoryId")]
        public int? CouponHistoryId { get; set; }

        [JsonPropertyName("couponHistory")]
        public CouponHistory CouponHistory { get; set; } = new CouponHistory()!;

        [JsonPropertyName("orderNote")]
        public string? OrderNote { get; set; }

        [JsonPropertyName("orderPay")]
        public int OrderPay { get; set; }

        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("orderStatus")]
        public int? OrderStatus { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("cargoCompany")]
        public string? CargoCompany { get; set; }

        [JsonPropertyName("cargoCode")]
        public string? CargoCode { get; set; }

        [JsonPropertyName("carts")]
        public List<Carts> Carts { get; set; } = new List<Carts>();

        [JsonPropertyName("paymentNotifications")]
        public List<PaymentNotification> PaymentNotifications { get; set; } = new List<PaymentNotification>();
    }
}
