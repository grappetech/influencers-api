using Newtonsoft.Json;

namespace Action.VewModels
{
	public class SocialPublicViewModel
	{
		[JsonProperty("socialEngagement")]
		public double SocialEngagement { get; set; }
		[JsonProperty("ageRanges")]
		public AgeRangesViewModel AgeRanges { get; set; } = new AgeRangesViewModel();
		[JsonProperty("males")]
		public double Males { get; set; }
		[JsonProperty("females")]
		public double Females { get; set; }
	}
}