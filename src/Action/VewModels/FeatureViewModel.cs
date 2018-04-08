using Newtonsoft.Json;

namespace Action.VewModels
{
	public class FeatureViewModel
	{
		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
