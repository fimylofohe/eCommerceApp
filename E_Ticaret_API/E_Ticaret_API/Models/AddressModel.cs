using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class AddressModel
    {
        [Display(Name = "Açık Adres *")]
        [Required(ErrorMessage = "Lütfen Açık Adres Giriniz")]
        public required string AddressText { get; set; }

        [Display(Name = "İl *")]
        [Required(ErrorMessage = "Lütfen İl Giriniz")]
        public required string Province { get; set; }

        [Display(Name = "İlçe *")]
        [Required(ErrorMessage = "Lütfen İlçe Giriniz")]
        public required string District { get; set; }

        [Display(Name = "Ülke *")]
        [Required(ErrorMessage = "Lütfen Ülke Giriniz")]
        public required string Country { get; set; }

        [Display(Name = "Posta Kodu *")]
        [Required(ErrorMessage = "Lütfen Posta Kodu Giriniz")]
        public required string PostalCode { get; set; }
    }
}
