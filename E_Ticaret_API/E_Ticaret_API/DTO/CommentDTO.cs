namespace E_Ticaret_API.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Status { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public ProductDTO Product { get; set; } = null!;
        public UserDTO User { get; set; } = null!;
    }
}