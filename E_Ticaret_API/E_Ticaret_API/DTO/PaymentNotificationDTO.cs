using E_Ticaret_API.Data;

namespace E_Ticaret_API.DTO
{
    public class PaymentNotificationDTO
    {
        public int PaymentId { get; set; }
        public int? OrderId { get; set; }
        public int? BankId { get; set; }
        public BankDTO Bank { get; set; } = null!;
        public string? NameSurname { get; set; }
        public double? TotalAmount { get; set; }
        public string? Receipt { get; set; }
        public string? PayNote { get; set; }
        public DateTime PayDate { get; set; }
        public bool Status { get; set; }
    }
}
