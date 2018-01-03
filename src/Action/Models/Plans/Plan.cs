using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Action.Models.Plans
{
	public class Plan
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public decimal Price { get; set; }
		public List<Features> Features { get; set; } = new List<Features>();
		public ETypePlan TypePlan { get; set; }
	}
}