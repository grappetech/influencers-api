using System;
using System.Collections.Generic;

namespace Action.Services.Watson.ToneAnalyze
{

    public class SentencesTone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public long? SentenceId { get; set; }
        public string Text { get; set; }
        public long? InputFrom { get; set; }
        public long? InputTo { get; set; }

        private List<ToneCategories> _toneCategories = new List<ToneCategories>();
        public List<ToneCategories> ToneCategories
        {
            get { return _toneCategories; }
            set { _toneCategories = value; }
        }
    }
}
