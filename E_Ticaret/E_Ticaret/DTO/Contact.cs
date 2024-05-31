using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Contact
    {
        [JsonPropertyName("contactId")]
        public int ContactId { get; set; }

        [JsonPropertyName("nameSurname")]
        public string? NameSurname { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("publishedDate")]
        public DateTime PublishedDate { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
