using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class CartModel
    {
        [Display(Name = "Adet")]
        [Required(ErrorMessage = "Lütfen adet giriniz!")]
        public required int Quantity { get; set; }
    }
}
