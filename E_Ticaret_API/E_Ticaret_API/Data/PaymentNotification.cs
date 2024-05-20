using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class PaymentNotification
    {
        [Key]
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int BankId { get; set; }
        public Bank Bank { get; set; } = null!;
        public string? NameSurname { get; set; }
        public double? TotalAmount { get; set; }
        public string? Receipt { get; set; }
        public string? PayNote { get; set; }
        public DateTime PayDate { get; set; }
        public bool Status { get; set; }
    }
}
