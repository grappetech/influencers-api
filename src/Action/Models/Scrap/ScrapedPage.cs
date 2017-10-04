using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Action.Models.Scrap
{
    public class ScrapedPage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey(nameof(ScrapSourceId))]
        public ScrapSource ScrapSource { get; set; }

        public int? ScrapSourceId { get; set; } = null;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public String Url { get; set; }
        public String Text { get; set; }
    }
}