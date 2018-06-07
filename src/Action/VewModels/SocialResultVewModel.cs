using System.Collections.Generic;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public abstract class SocialResultVewModel
    {
        [JsonProperty("followers")]
        public int Followers { get; set; }
        [JsonProperty("likes")]
        public int Likes { get; set; }
        [JsonProperty("engament")]
        public double Engagement { get; set; }
        [JsonProperty("ageRanges")]
        public AgeRangesViewModel AgeRanges { get; set; } = new AgeRangesViewModel();
        [JsonProperty("stats")]
        public List<SocialStatViewModel> Stats { get; set; } = new List<SocialStatViewModel>();
    }
}