using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }
        public string? Name { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNo { get; set; }
        public string? Branch { get; set; }
        public string? IBAN { get; set; }
        public bool Status { get; set; }

        public ICollection<PaymentNotification> PaymentNotifications { get; set; } = new List<PaymentNotification>();
    }
}
