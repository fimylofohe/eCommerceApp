namespace E_Ticaret_API.DTO
{
    public class ShopDTO
    {
        public int TotalProduct { get; set; }
        public int ProductPerPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalProduct / ProductPerPage);
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}
