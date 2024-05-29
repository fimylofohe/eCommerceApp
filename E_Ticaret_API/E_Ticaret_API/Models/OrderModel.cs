using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class OrderModel
    {
        [Display(Name = "Kargo Firması *")]
        public string? CargoCompany { get; set; }

        [Display(Name = "Kargo Takip Numarası *")]
        public string? CargoCode { get; set; }

        [Display(Name = "Sipariş Durumu *")]
        [Required(ErrorMessage = "Lütfen Sipariş Durumunu Seçiniz")]
        public required int OrderStatus { get; set; }
    }
}
