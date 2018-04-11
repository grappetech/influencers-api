using System;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public class RelationTypeViewModel
    {  
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
