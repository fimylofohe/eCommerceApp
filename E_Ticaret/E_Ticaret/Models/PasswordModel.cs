using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class PasswordModel
    {
        [Display(Name = "Eski Şifre")]
        [Required(ErrorMessage = "Lütfen Eski Şifreyi Giriniz")]
        public required string EPassword { get; set; }

        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "Lütfen Yeni Şifreyi Giriniz")]
        public required string Password { get; set; }

        [Display(Name = "Tekrar Yeni Şifre")]
        [Required(ErrorMessage = "Lütfen Tekar Yeni Şifreyi Giriniz")]
        public required string TPassword { get; set; }
    }
}
