using System.Collections.Generic;

namespace Action.VewModels
{
    public abstract class SocialResultVewModel
    {
        public int Followers { get; set; }
        public int Likes { get; set; }
        public double Engagement { get; set; }
        public AgeRangesViewModel AgeRanges { get; set; }
        public List<SocialStatViewModel> Stats { get; set; }
    }
}