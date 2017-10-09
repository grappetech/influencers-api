using System.ComponentModel.DataAnnotations;

namespace Action.Models
{
    public class Briefing
    {
        [Key]
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public string Factor { get; set; }
        [DataType("JSON")]
        public string Analysis { get; set; }
        public byte[] File { get; set; } = null;
    }
}