using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Blog
    {
        [JsonPropertyName("blogId")]
        public int BlogId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("seoURL")]
        public string? SeoURL { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("picture")]
        public string? Picture { get; set; }

        [JsonPropertyName("publishedDate")]
        public DateTime PublishedDate { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
