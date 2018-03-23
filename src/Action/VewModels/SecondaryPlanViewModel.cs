using System;

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
