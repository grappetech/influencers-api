using Newtonsoft.Json;

namespace Action.VewModels
{
    public class AddUserViewModel
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
        public string Surname { get; set; }
        [JsonProperty("profile")]
        public string Phone { get; set; }
    }
}