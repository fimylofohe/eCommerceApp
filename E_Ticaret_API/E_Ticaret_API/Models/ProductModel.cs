using E_Ticaret_API.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Ticaret_API.Models
{
    public class ProductModel
    {
        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Lütfen başlık giriniz!")]
        public required string? Name { get; set; }

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Lütfen açıklama giriniz!")]
        public required string? Description { get; set; }

        [Display(Name = "SKU")]
        [Required(ErrorMessage = "Lütfen sku giriniz!")]
        public required string? Sku { get; set; }

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Lütfen fiyat giriniz!")]
        public required double? Price { get; set; }

        [Display(Name = "Stok")]
        [Required(ErrorMessage = "Lütfen stok giriniz!")]
        public required int? Stock { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Lütfen kategori seçiniz!")]
        public required int CategoryId { get; set; }

        [Display(Name = "Durum")]
        public required bool Status { get; set; }

        [JsonPropertyName("pictures")]
        public List<Picture> Pictures { get; set; } = new List<Picture>();

        [JsonPropertyName("comments")]
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
