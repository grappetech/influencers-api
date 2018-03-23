using System.ComponentModel.DataAnnotations;

namespace Action.Models.Plans
{
    public class Features
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}