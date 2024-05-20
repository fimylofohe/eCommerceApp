using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
