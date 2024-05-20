using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class PaymentNotification
    {
        [JsonPropertyName("paymentId")]
        public int PaymentId { get; set; }

        [JsonPropertyName("orderId")]
        public int? OrderId { get; set; }

        [JsonPropertyName("bankId")]
        public int? BankId { get; set; }

        [JsonPropertyName("bank")]
        public Bank Bank { get; set; } = null!;

        [JsonPropertyName("nameSurname")]
        public string? NameSurname { get; set; }

        [JsonPropertyName("totalAmount")]
        public double? TotalAmount { get; set; }

        [JsonPropertyName("receipt")]
        public string? Receipt { get; set; }

        [JsonPropertyName("payNote")]
        public string? PayNote { get; set; }

        [JsonPropertyName("payDate")]
        public DateTime PayDate { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
