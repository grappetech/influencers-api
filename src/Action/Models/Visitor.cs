using System.ComponentModel.DataAnnotations;

namespace Action.Models
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}