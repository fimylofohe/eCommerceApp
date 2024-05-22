using System.ComponentModel.DataAnnotations;

namespace E_Ticaret_API.Models
{
    public class CommentModel
    {
        [Display(Name = "Yorum *")]
        [Required(ErrorMessage = "Lütfen İsim Giriniz")]
        public required string Text { get; set; }

        [Display(Name = "Yorum Durumu")]
        public required bool Status { get; set; }
    }
}
