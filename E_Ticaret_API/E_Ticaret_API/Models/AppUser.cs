using Microsoft.AspNetCore.Identity;

namespace E_Ticaret_API.Models
{
    public class AppUser:IdentityUser<int>
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DateAdded { get; set; }
    }
}