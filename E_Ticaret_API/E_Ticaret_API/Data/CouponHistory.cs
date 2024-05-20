using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace E_Ticaret_API.Data
{
    public class CouponHistory
    {
        [Key]
        public int CouponHistoryId { get; set; }
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; } = null!;
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public bool Status { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
