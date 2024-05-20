using E_Ticaret_API.Data;
using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.DTO
{
    public class AddressDTO
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string? AddressText { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }
}