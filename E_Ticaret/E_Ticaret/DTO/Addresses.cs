using E_Ticaret.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Addresses
    {
        [JsonPropertyName("addressId")]
        public int AddressId { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("addressText")]
        public string? AddressText { get; set; }

        [JsonPropertyName("province")]
        public string? Province { get; set; }

        [JsonPropertyName("district")]
        public string? District { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; }
    }
}
