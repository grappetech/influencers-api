using System;
using Action.Models.Core;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public class BriefingViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("ageRange")]
        public string AgeRange { get; set; }
        
        [JsonProperty("gender")]
        public string Gender { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
        
        [JsonProperty("persosnality")]
        public string Personality { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
        
        [JsonProperty("tone")]
        public string Tone { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("factor")]
        public string Factor { get; set; }
        
        [JsonProperty("entity")]
        public string Entity { get; set; }
        
        [JsonProperty("strength")]
        public decimal Strength { get; set; }
        
        [JsonProperty("documentUrl")]
        public string DocumentUrl { get; set; }
        
        [JsonProperty("status")]
        public EStatus? Status { get; set; }
        
        [JsonProperty("date")]
        public DateTime? Date { get; set; }
    }
}