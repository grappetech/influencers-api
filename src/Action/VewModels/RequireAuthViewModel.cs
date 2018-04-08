using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public class RequireAuthViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [Url]
        [Display(Name = "Url")]
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}