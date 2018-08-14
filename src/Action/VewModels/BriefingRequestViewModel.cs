using System.Collections.Generic;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public class BriefingRequestViewModel
    {
        
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("gender")]
            public string Gender { get; set; }

            [JsonProperty("role")]
            public List<string> Role { get; set; }

            [JsonProperty("factor")]
            public List<FactorViewModel> Factor { get; set; }

            [JsonProperty("targetGender")]
            public string TargetGender { get; set; }

            [JsonProperty("ageRange")]
            public string AgeRange { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("targetCity")]
            public string TargetCity { get; set; }
        }
}