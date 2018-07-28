using System;
using System.Collections.Generic;
using Action.Data.Models.Core;
using Action.Data.Models.Core.Plans;
using Action.Data.Models.Core.Watson;

namespace Action.Data.Models.Core.ServiceAccount
{
	public class Account
	{
		public int Id { get; set; }
		public DateTime ActivationDate { get; set; }
		public EAccountStatus Status { get; set; } = EAccountStatus.Active;
		public String Name { get; set; }
		public String CompanyName { get; set; }
		public Plan Plan { get; set; }
		public int? PlanId { get; set; }
		public User Administrator { get; set; }
		public string AdministratorId { get; set; }
		public List<User> Users { get; set; } = new List<User>();
		public List<Entity> Entities { get; set; }
		public List<SecondaryPlan> SecondaryPlans { get; set; } = new List<SecondaryPlan>();
		private string _imageUrl;
		public string ImageUrl
		{
			get
			{
				if (String.IsNullOrEmpty(_imageUrl))
					return "";
				else
					return _imageUrl;
			}
			set { _imageUrl = value; }
		}
	}
}