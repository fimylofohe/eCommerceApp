using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class SliderModel
    {
        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Lütfen başlık giriniz!")]
        public required string Title { get; set; }

        [Display(Name = "Alt Başlık")]
        [Required(ErrorMessage = "Lütfen alt başlık seçiniz!")]
        public required string SubTitle { get; set; }

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Lütfen açıklama yazınız!")]
        public required string Description { get; set; }

        [Display(Name = "Buton Yazısı")]
        [Required(ErrorMessage = "Lütfen buton yazısı giriniz!")]
        public required string ButtonTitle { get; set; }

        [Display(Name = "Buton Url")]
        [Required(ErrorMessage = "Lütfen buton url giriniz!")]
        public required string ButtonUrl { get; set; }

        [Display(Name = "Slider Görseli")]
        public string? BackgroundImg { get; set; } = null;

        [Display(Name = "Slider Durumu")]
        public required bool Status { get; set; }
    }
}
