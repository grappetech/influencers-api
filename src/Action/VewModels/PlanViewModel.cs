using System.Collections.Generic;
using Newtonsoft.Json;

namespace Action.VewModels
{
	public class PlanViewModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("slug")]
		public string Slug { get; set; }
		[JsonProperty("price")]
		public decimal Price { get; set; }
		[JsonProperty("features")]
		public List<FeatureViewModel> Features { get; set; } = new List<FeatureViewModel>();
	}
}
