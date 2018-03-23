using System;
using System.Collections.Generic;

namespace Action.Models.Watson.ToneAnalyze
{
    public class ToneCategories
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public List<Tone> Tones { get; set; } = new List<Tone>();

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}