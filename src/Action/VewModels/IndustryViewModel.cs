using Newtonsoft.Json;

namespace Action.VewModels
{
    public class IndustryViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}