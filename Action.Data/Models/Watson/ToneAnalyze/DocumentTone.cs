using System;
using System.Collections.Generic;

namespace Action.Data.Models.Core.Watson.ToneAnalyze
{
    public class DocumentTone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public List<ToneCategories> ToneCategories { get; set; } = new List<ToneCategories>();
    }
}