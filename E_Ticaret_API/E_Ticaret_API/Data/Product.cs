using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string? SKU { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Stock {  get; set; }
        public bool Status { get; set; }

        public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
