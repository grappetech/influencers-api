using System;

namespace Action.Models.Watson.NLU
{
    public class SentimentKeyword
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public float? score { get; set; }
    }
}