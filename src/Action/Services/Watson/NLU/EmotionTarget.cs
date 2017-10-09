using System;

namespace Action.Services.Watson.NLU
{
    public class EmotionTarget
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public EmotionDetail EmotionDetail { get; set; }
    }
}