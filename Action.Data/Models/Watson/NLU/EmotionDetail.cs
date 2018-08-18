using System;

namespace Action.Data.Models.Watson.NLU
{
    public class EmotionDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public float? Sadness { get; set; }
        public float? Joy { get; set; }
        public float? Fear { get; set; }
        public float? Disgust { get; set; }
        public float? Anger { get; set; }
    }
}