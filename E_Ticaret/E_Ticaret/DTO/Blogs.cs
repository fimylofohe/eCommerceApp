using E_Ticaret.DTO;
using System.Text.Json.Serialization;
namespace E_Ticaret.DTO
{
    public class Blogs
    {
        [JsonPropertyName("totalProduct")]
        public int TotalProduct { get; set; }

        [JsonPropertyName("productPerPage")]
        public int ProductPerPage { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("blogs")]
        public List<Blog> Blogsc { get; set; } = new List<Blog>();
    }
}