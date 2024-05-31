using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class ContactModel
    {
        [Display(Name = "İsim Soyisim *")]
        [Required(ErrorMessage = "Lütfen İsim Soyisim Giriniz")]
        public required string NameSurname { get; set; }

        [Display(Name = "E-Posta *")]
        [Required(ErrorMessage = "Lütfen E-Posta Giriniz")]
        public required string Email { get; set; }

        [Display(Name = "Konu *")]
        [Required(ErrorMessage = "Lütfen Konu Giriniz")]
        public required string Subject { get; set; }

        [Display(Name = "Mesaj *")]
        [Required(ErrorMessage = "Lütfen Mesaj Giriniz")]
        public required string Text { get; set; }

        [Display(Name = "Robot Doğrulaması *")]
        [Required(ErrorMessage = "Lütfen Robot Doğrulamasını Sağlayınız")]
        public required string gRecaptcha { get; set; }
    }
}
