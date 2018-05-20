using System;
using Newtonsoft.Json;

namespace Action.Models.Watson.NLU
{
    public class Keywords
    {
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string language { get; set; }
        public string retrieved_url { get; set; }
        public string fragment { get; set; }
        public string translatedFragment { get; set; }
        public string text { get; set; }
        public SentimentKeyword sentiment { get; set; }
        public float? relevance { get; set; }
        public EmotionsKeyword emotions { get; set; }
    }
}