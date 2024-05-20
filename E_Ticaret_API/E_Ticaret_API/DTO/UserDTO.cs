using E_Ticaret_API.Data;

namespace E_Ticaret_API.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string NameSurname
        {
            get
            {
                return this.Name + " " + this.Surname;
            }
        }
        public string? Email { get; set; }
        //public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Admin { get; set; }
        public bool Status { get; set; }

        public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();
        public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
        public List<CartDTO> Carts { get; set; } = new List<CartDTO>();
    }
}
