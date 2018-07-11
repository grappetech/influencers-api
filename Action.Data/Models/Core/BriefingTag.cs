using System.ComponentModel.DataAnnotations;

namespace Action.Data.Models.Core
{
    public class BriefingTag
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(180)]
        public string Text { get; set; }
    }
}