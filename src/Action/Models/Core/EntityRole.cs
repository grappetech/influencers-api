using System.ComponentModel.DataAnnotations;

namespace Action.Models.Core
{
    public class EntityRole
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(180)]
        public string Name { get; set; }
    }
}