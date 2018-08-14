using System.ComponentModel.DataAnnotations;

namespace Action.Data.Models.Core
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}