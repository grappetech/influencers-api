using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Action.Models.Watson.ToneAnalyze
{
    public class SentencesTone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("sentence_id")]
        public long? SentenceId { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("input_from")]
        public long? InputFrom { get; set; }
        [JsonProperty("input_to")]
        public long? InputTo { get; set; }
        [JsonProperty("tone_categories")]
        public List<ToneCategories> ToneCategories { get; set; } = new List<ToneCategories>();
    }
}