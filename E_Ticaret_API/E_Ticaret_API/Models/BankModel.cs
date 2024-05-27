using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class BankModel
    {
        [Display(Name = "Banka Adı *")]
        [Required(ErrorMessage = "Lütfen banka adı Giriniz")]
        public required string Name { get; set; }

        [Display(Name = "Hesap Adı *")]
        [Required(ErrorMessage = "Lütfen Hesap Sahibi Adı Giriniz")]
        public required string AccountName { get; set; }

        [Display(Name = "Hesap Numarası *")]
        [Required(ErrorMessage = "Lütfen Hesap Numarası Giriniz")]
        public required string AccountNo { get; set; }

        [Display(Name = "Şube *")]
        [Required(ErrorMessage = "Lütfen Şube Giriniz")]
        public required string Branch { get; set; }

        [Display(Name = "IBAN *")]
        [Required(ErrorMessage = "Lütfen IBAN Giriniz")]
        public required string IBAN { get; set; }

        [Display(Name = "Banka Durumu")]
        public required bool Status { get; set; }
    }
}
