using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Slider
    {
        [JsonPropertyName("sliderId")]
        public int SliderId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("subTitle")]
        public string? SubTitle { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("buttonTitle")]
        public string? ButtonTitle { get; set; }

        [JsonPropertyName("buttonUrl")]
        public string? ButtonUrl { get; set; }

        [JsonPropertyName("backgroundImg")]
        public string? BackgroundImg { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
