using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Action.Models.Watson.ToneAnalyze
{
    public class ToneCategories
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("tones")]
        public List<Tone> Tones { get; set; } = new List<Tone>();
        [JsonProperty("category_id")]
        public string CategoryId { get; set; }
        [JsonProperty("category_name")]
        public string CategoryName { get; set; }
    }
}