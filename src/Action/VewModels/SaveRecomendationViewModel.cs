using Newtonsoft.Json;

namespace Action.VewModels
{
    public class SaveRecomendationViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}