namespace E_Ticaret_API.Models
{
    public class PayNotModel
    {
        public required int OrderId { get; set; }
        public required int BankId { get; set; }
        public required string NameSurname { get; set; }
        public required double TotalAmount { get; set; }
        public required string Receipt { get; set; }
        public string? PayNote { get; set; }
    }
}
