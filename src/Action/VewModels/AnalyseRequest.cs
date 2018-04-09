using Newtonsoft.Json;

namespace Action.VewModels
{
    public class AnalyseRequest
    {
        [JsonProperty("brand")]
        public string Brand { get; set; }
        [JsonProperty("product")]
        public string Product { get; set; }
        [JsonProperty("briefing")]
        public string Briefing { get; set; }
        [JsonProperty("factor")]
        public string Factor { get; set; }
        [JsonProperty("file")]
        public byte[] File { get; set; }
    }
}