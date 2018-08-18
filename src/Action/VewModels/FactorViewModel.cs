using Newtonsoft.Json;

namespace Action.VewModels
{
    public partial class FactorViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}