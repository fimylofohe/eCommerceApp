using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string? NameSurname { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Status { get; set; }
    }
}
