using System;
using Newtonsoft.Json;

namespace Action.VewModels
{
	public class SecondaryPlanViewModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("price")]
		public decimal Price { get; set; }
		[JsonProperty("startDate")]
		public DateTime StartDate { get; set; }
		[JsonProperty("allowedUsers")]
		public int AllowedUsers { get; set; }
		[JsonProperty("account")]
		public AccountViewModel Account { get; set; }
	}
}
