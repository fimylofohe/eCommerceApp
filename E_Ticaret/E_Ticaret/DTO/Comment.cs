using E_Ticaret.DTO;
using System.Text.Json.Serialization;
namespace E_Ticaret.DTO
{
    public class Comment
    {
        [JsonPropertyName("commentId")]
        public int CommentId { get; set; }

        [JsonPropertyName("publishedDate")]
        public DateTime PublishedDate { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; } = null!;

        [JsonPropertyName("user")]
        public User User { get; set; } = null!;
    }
}