using System;
using Newtonsoft.Json;

namespace Action.Models.Watson.ToneAnalyze
{
    public class Tone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("score")]
        public double? Score { get; set; }
        [JsonProperty("tone_id")]
        public string ToneId { get; set; }
        [JsonProperty("tone_name")]
        public string ToneName { get; set; }
    }
}