using System;
using System.ComponentModel.DataAnnotations;

namespace Action.Models.Watson
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Alias { get; set; }
        public ECategory CategoryId { get; set; }
        public string Category => Enum.GetName(typeof(ECategory), CategoryId);
        public DateTime Date { get; set; } = DateTime.Today;
    }
}