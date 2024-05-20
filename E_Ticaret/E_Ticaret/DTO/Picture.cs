using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Picture
    {
        [JsonPropertyName("pictureId")]
        public int PictureId { get; set; }

        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }
    }
}
