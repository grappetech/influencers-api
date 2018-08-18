using System;
using System.ComponentModel.DataAnnotations;

namespace Action.Data.Models.Core.Scrap
{
    public class EntityMentions
    {
        [Key]
        public long Id { get; set; }

        public long EntityId { get; set; }
        public int ScrapSourceId { get; set; }
        public Guid ScrapedPageId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}