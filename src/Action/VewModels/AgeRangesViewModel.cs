using Newtonsoft.Json;

namespace Action.VewModels
{
    public class AgeRangesViewModel
    {
        
        [JsonProperty("age18_24")]
        public double Age18_24 { get; set; }
        [JsonProperty("age25_31")]
        public double Age25_31 { get; set; }
        [JsonProperty("age32_38")]
        public double Age32_38 { get; set; }
        [JsonProperty("age39_45")]
        public double Age39_45 { get; set; }
        [JsonProperty("age46_52")]
        public double Age46_52 { get; set; }
        [JsonProperty("age53_59")]
        public double Age53_59 { get; set; }
        [JsonProperty("age60")]
        public double Age60 { get; set; }
    }
}