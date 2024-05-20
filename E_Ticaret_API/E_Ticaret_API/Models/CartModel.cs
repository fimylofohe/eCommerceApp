using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class CartModel
    {
        [Display(Name = "Adet")]
        [Required(ErrorMessage = "Lütfen adet giriniz!")]
        public required int Quantity { get; set; }
    }
}
