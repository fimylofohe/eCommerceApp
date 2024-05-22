using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Data
{
    public class Setting
    {
        [Key]
        public int SettingId { get; set; }
        public string? Mkey { get; set; }
        public string? Mval { get; set; }
    }
}
