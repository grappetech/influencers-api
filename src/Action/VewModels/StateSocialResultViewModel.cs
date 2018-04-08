using Newtonsoft.Json;

namespace Action.VewModels
{
    public class StateSocialResultViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("score")]
        public double Score { get; set; }
    }
}