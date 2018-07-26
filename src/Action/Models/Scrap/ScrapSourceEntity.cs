using Action.Models.Watson;

namespace Action.Models.Scrap
{
    public class ScrapSourceEntity
    {
        public int ScrapSourceId { get; set; }
        public long EntityId { get; set; }
        public Entity Entity { get; set; }
        public ScrapSource ScrapSource { get; set; }
    }
}
