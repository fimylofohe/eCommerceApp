using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class BlogModel
    {
        [Display(Name = "Blog Başlığı *")]
        [Required(ErrorMessage = "Lütfen Blog Başlığı Giriniz")]
        public required string Title { get; set; }

        [Display(Name = "Blog İçeriği *")]
        [Required(ErrorMessage = "Lütfen Blog İçeriği Giriniz")]
        public required string Text { get; set; }

        [Display(Name = "Görsel *")]
        public string? Picture { get; set; }

        [Display(Name = "Blog Durumu")]
        public required bool Status { get; set; }
    }
}
