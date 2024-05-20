using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public string? ButtonTitle { get; set; }
        public string? ButtonUrl { get; set; }
        public string? BackgroundImg { get; set; }
        public bool Status { get; set; }
    }
}
