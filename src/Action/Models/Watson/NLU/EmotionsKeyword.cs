using System;

namespace Action.Models.Watson.NLU
{
    public class EmotionsKeyword
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double sadness { get; set; }
        public double joy { get; set; }
        public double fear { get; set; }
        public double disgust { get; set; }
        public double? anger { get; set; }
    }
}