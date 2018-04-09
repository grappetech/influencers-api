using Newtonsoft.Json;

namespace Action.VewModels
{
    public class CitySocialResultViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
        
        [JsonProperty("score")]
        public double Score { get; set; }
    }
}