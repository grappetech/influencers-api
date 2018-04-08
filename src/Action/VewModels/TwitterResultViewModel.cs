using Newtonsoft.Json;

namespace Action.VewModels
{
    public class TwitterResultViewModel : SocialResultVewModel
    {
        [JsonProperty("retweets")]
        public int Retweets { get; set; }
    }
}