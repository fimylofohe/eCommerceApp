using E_Ticaret_API.Data;

namespace E_Ticaret_API.DTO
{
    public class CouponHistoryDTO
    {
        public int CouponHistoryId { get; set; }
        public int CouponId { get; set; }
        public CouponDTO Coupon { get; set; } = null!;
        public int UserId { get; set; }
        public UserDTO User { get; set; } = null!;
        public int OrderId { get; set; }
        public bool Status { get; set; }
    }
}
