using E_Ticaret.DTO;
using System.Text.Json.Serialization;
namespace E_Ticaret.DTO
{
    public class User
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("surname")]
        public string? Surname { get; set; }

        [JsonPropertyName("nameSurname")]
        public string? NameSurname { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("admin")]
        public bool Admin { get; set; }

        [JsonPropertyName("status")]
        public bool? Status { get; set; }

        [JsonPropertyName("addresses")]
        public List<Addresses> Addresses { get; set; } = new List<Addresses>();

        [JsonPropertyName("orders")]
        public List<Orders> Orders { get; set; } = new List<Orders>();

        [JsonPropertyName("comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        [JsonPropertyName("carts")]
        public List<Carts> Carts { get; set; } = new List<Carts>();
    }
}