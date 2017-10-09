using System;

namespace Action.Services.Watson.NLU
{
    public class EmotionDocument
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public EmotionDetail EmotionDetail { get; set; }
    }
}