using System;
using System.Collections.Generic;

namespace Action.Models.Watson.ToneAnalyze
{
    public class SentencesTone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public long? SentenceId { get; set; }
        public string Text { get; set; }
        public long? InputFrom { get; set; }
        public long? InputTo { get; set; }

        public List<ToneCategories> ToneCategories { get; set; } = new List<ToneCategories>();
    }
}