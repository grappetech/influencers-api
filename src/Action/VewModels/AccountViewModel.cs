
using System;
using System.Collections.Generic;

namespace Action.VewModels
{
	public class AccountViewModel
	{
		public int Id { get; set; }
		public String CompanyName { get; set; }
		public PlanViewModel Plan { get; set; }
		public List<SecondaryPlanViewModel> SecondaryPlans { get; set; } = new List<SecondaryPlanViewModel>();
		public List<EntityViewModel> Entities { get; set; } = new List<EntityViewModel>();
		public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
		public string Status { get; set; }
	}
}
