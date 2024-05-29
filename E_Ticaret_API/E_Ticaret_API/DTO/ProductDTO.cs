namespace E_Ticaret_API.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string? SKU { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        public bool Status { get; set; }
        public string? SeoURL { get; set; }

        public CategoryDTO Category { get; set; } = null!;
        public List<PictureDTO> Pictures { get; set; } = new List<PictureDTO>();
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
