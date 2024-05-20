using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class RegisterModel
    {
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "Lütfen Adınızı giriniz!")]
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [Display(Name = "Soyisim")]
        [Required(ErrorMessage = "Lütfen Soyadınızı giriniz!")]
        [DataType(DataType.Text)]
        public required string Surname { get; set; }

        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "Lütfen Mail adresinizi giriniz!")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "Lütfen Telefon Numaranızı giriniz!")]
        [DataType(DataType.PhoneNumber)]
        public required string PhoneNumber { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Lütfen parolanızı giriniz!")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
