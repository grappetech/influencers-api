
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Action.VewModels
{
	public class AccountViewModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("companyName")]
		public String CompanyName { get; set; }
		[JsonProperty("plan")]
		public PlanViewModel Plan { get; set; }
		[JsonProperty("secondaryPlans")]
		public List<SecondaryPlanViewModel> SecondaryPlans { get; set; } = new List<SecondaryPlanViewModel>();
		[JsonProperty("entities")]
		public List<EntityViewModel> Entities { get; set; } = new List<EntityViewModel>();
		[JsonProperty("users")]
		public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
		[JsonProperty("status")]
		public string Status { get; set; }
	}
}
