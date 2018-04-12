using Newtonsoft.Json;

namespace Action.VewModels
{
    public class WordViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("text")] 
        public string Text { get; set; }
        [JsonProperty("size")] 
        public int Weight { get; set; }
        [JsonProperty("type")] 
        public string Type { get; set; }
    }
}