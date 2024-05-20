using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Picture
    {
        [Key]
        public int PictureId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string? Path { get; set; }
    }
}
