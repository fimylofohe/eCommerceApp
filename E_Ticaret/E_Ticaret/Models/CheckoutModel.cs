using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class CheckoutModel
    {
        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Lütfen adres seçiniz!")]
        public required int AddressId { get; set; }

        [Display(Name = "Sipariş Notu")]
        public string? OrderNote { get; set; }

        [Display(Name = "Ödeme Yöntemi")]
        [Required(ErrorMessage = "Lütfen ödeme yöntemi seçiniz!")]
        public required int OrderPay { get; set; }
    }
}
