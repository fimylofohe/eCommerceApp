using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string? OrderKey { get; set; }
        public double? Amount { get; set; }
        public double? CouponAmount { get; set; }
        public double? TotalAmount { get; set; }
        public int? CouponHistoryId { get; set; }
        public CouponHistory CouponHistory { get; set; } = null!;
        public string? OrderNote { get; set; }
        public int OrderPay { get; set; }
        public DateTime OrderDate { get; set; }
        public int? OrderStatus { get; set; }
        public bool Status { get; set; }
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<PaymentNotification> PaymentNotifications { get; set; } = new List<PaymentNotification>();
    }
}