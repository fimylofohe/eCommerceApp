namespace E_Ticaret_API.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }

        public int? Products { get; set; }
    }
}