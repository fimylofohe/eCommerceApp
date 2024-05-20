using System.Text.Json.Serialization;

namespace E_Ticaret.DTO
{
    public class Bank
    {
        [JsonPropertyName("bankId")]
        public int BankId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("accountName")]
        public string? AccountName { get; set; }

        [JsonPropertyName("accountNo")]
        public string? AccountNo { get; set; }

        [JsonPropertyName("branch")]
        public string? Branch { get; set; }

        [JsonPropertyName("iban")]
        public string? IBAN { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
