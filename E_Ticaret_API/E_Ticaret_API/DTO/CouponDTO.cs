using E_Ticaret_API.Data;
using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.DTO
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int? DiscountAmount { get; set; }
        public string? CouponCode { get; set; }
        public DateTime? ValidityDate { get; set; }
        public bool SingleUse { get; set; }
        public bool Status { get; set; }
    }
}
