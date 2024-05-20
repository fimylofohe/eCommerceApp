using E_Ticaret_API.Data;

namespace E_Ticaret_API.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int AddressId { get; set; }
        public AddressDTO Address { get; set; } = new AddressDTO();
        public int UserId { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
        public string? OrderKey { get; set; }
        public double? Amount { get; set; }
        public double? CouponAmount { get; set; }
        public double? TotalAmount { get; set; }
        public int? CouponHistoryId { get; set; }
        public CouponHistoryDTO CouponHistory { get; set; } = null!;
        public string? OrderNote { get; set; }
        public int OrderPay { get; set; }
        public DateTime OrderDate { get; set; }
        public int? OrderStatus { get; set; }
        public bool Status { get; set; }
        public List<CartDTO> Carts { get; set; } = new List<CartDTO>();
        public List<PaymentNotificationDTO> PaymentNotifications { get; set; } = new List<PaymentNotificationDTO>();
    }
}
