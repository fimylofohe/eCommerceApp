using E_Ticaret.DTO;
using System.Text.Json.Serialization;
namespace E_Ticaret.DTO
{
    public class Shop
    {
        [JsonPropertyName("totalProduct")]
        public int TotalProduct { get; set; }

        [JsonPropertyName("productPerPage")]
        public int ProductPerPage { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("products")]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}