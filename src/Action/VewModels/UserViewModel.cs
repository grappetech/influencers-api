using Newtonsoft.Json;

namespace Action.VewModels
{
	public class UserViewModel
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("accountId")]
		public int AccountId { get; set; }
		[JsonProperty("role")]
		public string Role { get; set; }
		[JsonProperty("email")]
		public string Email { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("surname")]
		public string SurName { get; set; }
		[JsonProperty("phone")]
		public string Phone { get; set; }
		[JsonProperty("administrator")]
		public bool Administrator { get; set; }
	}
}
