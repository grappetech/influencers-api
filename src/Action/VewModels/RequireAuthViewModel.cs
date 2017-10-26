using System.ComponentModel.DataAnnotations;

namespace Action.VewModels
{
    public class RequireAuthViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}