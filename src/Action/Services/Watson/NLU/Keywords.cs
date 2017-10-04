using System;

namespace Action.Services.Watson.NLU
{
    public class Keywords
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string language { get; set; }
        public string retrieved_url { get; set; }
        public string text { get; set; }
        public SentimentKeyword sentiment { get; set; }
        public float? relevance { get; set; }
        public EmotionsKeyword emotions { get; set; }
    }
}
