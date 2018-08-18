using Action.Data.Models.Core.Plans;
using Action.Data.Models.Core.ServiceAccount;
using Microsoft.AspNetCore.Identity;

namespace Action.Data.Models.Core
{
    public class User : IdentityUser
    {
		public string Name { get; set; }
		public string Surname { get; set; }
		public Plan Plan { get; set; }
		public int? PlanId { get; set; }
		public Account Account { get; set; }
		public int? AccountId { get; set; }
	}
}