using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class RegisterModel
    {
        [DataType(DataType.Text)]
        public required string Name { get; set; }

        [DataType(DataType.Text)]
        public required string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public required string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
