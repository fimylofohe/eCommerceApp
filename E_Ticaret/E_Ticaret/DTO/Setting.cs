using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Setting
    {
        [JsonPropertyName("settingId")]
        public int SettingId { get; set; }

        [JsonPropertyName("mkey")]
        public string? Mkey { get; set; }

        [JsonPropertyName("nval")]
        public string? Mval { get; set; }
    }
}
