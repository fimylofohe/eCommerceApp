using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace E_Ticaret_API.Data
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int? DiscountAmount { get; set; }
        public string? CouponCode { get; set; }
        public DateTime? ValidityDate { get; set; }
        public bool SingleUse { get; set; }
        public bool Status { get; set; }

        public ICollection<CouponHistory> CouponHistorys { get; set; } = new List<CouponHistory>();
    }
}
