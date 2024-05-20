using E_Ticaret.DTO;
using System.Text.Json.Serialization;
namespace E_Ticaret.DTO
{
    public class Product
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("sku")]
        public string? Sku { get; set; }

        [JsonPropertyName("price")]
        public double? Price { get; set; }

        [JsonPropertyName("stock")]
        public int? Stock { get; set; }

        [JsonPropertyName("status")]
        public bool? Status { get; set; }

        [JsonPropertyName("pictures")]
        public List<Picture> Pictures { get; set; } = new List<Picture>();

        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}