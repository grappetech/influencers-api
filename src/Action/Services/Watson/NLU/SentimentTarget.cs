using System;

namespace Action.Services.Watson.NLU
{
    public class SentimentTarget
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public float? Score { get; set; }
        public string Label { get; set; }
    }
}