﻿using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class UserModel
    {
        [Display(Name = "İsim *")]
        [Required(ErrorMessage = "Lütfen İsim Giriniz")]
        public required string Name { get; set; }

        [Display(Name = "Soyisim *")]
        [Required(ErrorMessage = "Lütfen Soyisim Giriniz")]
        public required string Surname { get; set; }

        [Display(Name = "E-Posta *")]
        [Required(ErrorMessage = "Lütfen E-Posta Adresi Giriniz")]
        public required string Email { get; set; }

        [Display(Name = "Telefon Numarası *")]
        [Required(ErrorMessage = "Lütfen Telefon Numarası Giriniz")]
        public required string PhoneNumber { get; set; }

        [Display(Name = "Hesap Durumu")]
        public required bool Status { get; set; }

        [Display(Name = "Yetki Durumu")]
        public required bool Admin { get; set; }
    }
}
