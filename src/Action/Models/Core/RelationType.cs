using System.ComponentModel.DataAnnotations;

namespace Action.Models.Core
{
    public class RelationType
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}