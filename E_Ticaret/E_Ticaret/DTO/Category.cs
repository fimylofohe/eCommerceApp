using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Category
    {
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("status")]
        public bool? Status { get; set; }

        [JsonPropertyName("products")]
        public int? Products { get; set; }
    }
}
