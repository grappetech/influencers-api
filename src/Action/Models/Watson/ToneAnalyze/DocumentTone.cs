using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Action.Models.Watson.ToneAnalyze
{
    public class DocumentTone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("tone_categories")]
        public List<ToneCategories> ToneCategories { get; set; } = new List<ToneCategories>();
    }
}