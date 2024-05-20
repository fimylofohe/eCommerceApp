using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class LoginModel
    {
        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "Lütfen mail adresinizi giriniz!")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Lütfen parolanızı giriniz!")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
