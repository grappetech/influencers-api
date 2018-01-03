using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action.VewModels
{
	public class SecondaryPlanViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public DateTime StartDate { get; set; }
		public int AllowedUsers { get; set; }
		public AccountViewModel Account { get; set; }
	}
}
