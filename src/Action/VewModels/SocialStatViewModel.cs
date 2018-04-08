using Newtonsoft.Json;

namespace Action.VewModels
{
    public class SocialStatViewModel
    {
        [JsonProperty("month")]
        public string Month { get; set; }
        [JsonProperty("followers")]
        public int Followers { get; set; }
        [JsonProperty("retweets")]
        public int Retweets { get; set; }
        [JsonProperty("likes")]
        public int Likes { get; set; }
        [JsonProperty("engagement")]
        public double Engagement { get; set; }
    }
}