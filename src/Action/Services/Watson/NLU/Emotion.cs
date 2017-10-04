using System;
using System.Collections.Generic;

namespace Action.Services.Watson.NLU
{
    public partial class Emotion
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        private List<EmotionTarget> _EmotionTarget = new List<EmotionTarget>();
        public List<EmotionTarget> EmotionTarget
        {
            get { return _EmotionTarget; }
            set { _EmotionTarget = value; }
        }

        public EmotionDocument EmotionDocument { get; set; }
    }
}
