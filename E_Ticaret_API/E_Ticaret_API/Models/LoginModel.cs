using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class LoginModel
    {
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
