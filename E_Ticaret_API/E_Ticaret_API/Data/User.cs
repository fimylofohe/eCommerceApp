using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace E_Ticaret_API.Data
{
    public class User
    {
        [Key]
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
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Admin { get; set; }
        public bool Status { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
