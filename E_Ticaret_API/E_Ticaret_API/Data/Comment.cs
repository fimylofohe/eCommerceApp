using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string? Text { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Status { get; set; }
    }
}
