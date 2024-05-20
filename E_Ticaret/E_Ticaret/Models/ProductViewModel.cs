using E_Ticaret.DTO;
using Microsoft.Extensions.Hosting;

namespace E_Ticaret.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } = new();
    }
}
