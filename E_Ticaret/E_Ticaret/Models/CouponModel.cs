using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class CouponModel
    {
        [Display(Name = "Kupon Kodu")]
        [Required(ErrorMessage = "Lütfen kod giriniz!")]
        public required string Code { get; set; }
    }
}
