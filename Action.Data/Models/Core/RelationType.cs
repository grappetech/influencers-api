using System.ComponentModel.DataAnnotations;

namespace Action.Data.Models.Core
{
    public class RelationType
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}