using Newtonsoft.Json;

namespace Action.VewModels
{
    public class EntityPersonalityRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}