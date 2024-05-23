using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class CouponsModel
    {
        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Lütfen başlık giriniz!")]
        public required string Name { get; set; }

        [Display(Name = "Tip")]
        [Required(ErrorMessage = "Lütfen tip seçiniz!")]
        public required string Type { get; set; }

        [Display(Name = "İndirim Miktarı")]
        [Required(ErrorMessage = "Lütfen İndirim Miktarı yazınız!")]
        public required int DiscountAmount { get; set; }

        [Display(Name = "Kupon Kodu")]
        [Required(ErrorMessage = "Lütfen kod giriniz!")]
        public required string CouponCode { get; set; }

        [Display(Name = "Son Kullanım Tarihi")]
        [Required(ErrorMessage = "Lütfen son kullanım tarihi giriniz!")]
        public required DateTime ValidityDate { get; set; }

        [Display(Name = "Tek Kullanım")]
        [Required(ErrorMessage = "Lütfen Tek Kullanım durumunu seçiniz!")]
        public required bool SingleUse { get; set; }

        [Display(Name = "Kupon Durumu")]
        public required bool Status { get; set; }
    }
}
