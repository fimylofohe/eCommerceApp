using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class CategoryModel
    {
        [Display(Name = "Başlık *")]
        [Required(ErrorMessage = "Lütfen Başlık Giriniz")]
        public required string Name { get; set; }

        [Display(Name = "Durum")]
        public required bool Status { get; set; }
    }
}
