using System;
using System.Collections.Generic;

namespace Action.Models.Watson.NLU
{
    public class Emotion
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public List<EmotionTarget> EmotionTarget { get; set; } = new List<EmotionTarget>();

        public EmotionDocument EmotionDocument { get; set; }
    }
}