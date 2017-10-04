using System;
using System.Collections.Generic;

namespace Action.Services.Watson.ToneAnalyze
{
    public class DocumentTone
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        private List<ToneCategories> _toneCategories = new List<ToneCategories>();
        public List<ToneCategories> ToneCategories
        {
            get { return _toneCategories; }
            set { _toneCategories = value; }
        }
    }
}
