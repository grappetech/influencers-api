using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace Action.Models.Plans
{
    public class Features
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}