using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class ApiStatus
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("msg")]
        public string? Msg { get; set; }
    }
}
