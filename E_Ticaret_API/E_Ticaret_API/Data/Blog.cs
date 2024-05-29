using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? SeoURL { get; set; }
        public string? Text { get; set; }
        public string? Picture { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Status { get; set; }
    }
}
