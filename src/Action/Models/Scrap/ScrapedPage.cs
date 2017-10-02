using System;

namespace Action.Models.Scrap
{
    public class ScrapedPage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ScrapSource ScrapSource { get; set; }
        public int ScrapSourceId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public String Url { get; set; }
        public String Text { get; set; }
    }
}