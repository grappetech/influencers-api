using Action.Data.Models.Core.ServiceAccount;
using System;
using System.ComponentModel.DataAnnotations;

namespace Action.Data.Models.Core.Plans
{
	public class SecondaryPlan
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public DateTime StartDate { get; set; }
		public int AllowedUsers { get; set; }
		public Account Account { get; set; }
		public int? AccountId { get; set; }
	}
}
