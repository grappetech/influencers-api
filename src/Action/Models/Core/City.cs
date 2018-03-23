using System.ComponentModel.DataAnnotations;

namespace Action.Models.Core
{
    public class City
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public int StateId { get; set; }
    }
}