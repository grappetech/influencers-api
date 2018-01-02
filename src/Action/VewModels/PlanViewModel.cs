using System.Collections.Generic;

namespace Action.VewModels
{
	public class PlanViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public decimal Price { get; set; }
		public List<FeatureViewModel> Features { get; set; } = new List<FeatureViewModel>();
	}
}
