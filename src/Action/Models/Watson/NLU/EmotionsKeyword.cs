using System;

namespace Action.Models.Watson.NLU
{
    public class EmotionsKeyword
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public float? sadness { get; set; }
        public float? joy { get; set; }
        public float? fear { get; set; }
        public float? disgust { get; set; }
        public float? anger { get; set; }
    }
}