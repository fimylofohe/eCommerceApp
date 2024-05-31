using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace E_Ticaret_API.DTO
{
    public class ContactDTO
    {
        public int ContactId { get; set; }
        public string? NameSurname { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Status { get; set; }
    }
}
