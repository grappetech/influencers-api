using Newtonsoft.Json;

namespace Action.VewModels
{
    public class SimpleEntity
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("entity")]
        public string Entity { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}