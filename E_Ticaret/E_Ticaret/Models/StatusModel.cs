using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class StatusModel
    {

        [Display(Name = "Durum")]
        public required bool Status { get; set; }
    }
}
