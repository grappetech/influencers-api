using System;
using System.Collections.Generic;

namespace Action.Services.Watson.ToneAnalyze
{
    public class ToneCategories
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        private List<Tone> _Tones = new List<Tone>();
        public List<Tone> Tones
        {
            get { return _Tones; }
            set { _Tones = value; }
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}
