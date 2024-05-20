using E_Ticaret.DTO;
using System.Text.Json.Serialization;
namespace E_Ticaret.DTO
{
    public class Login
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }
    }
}