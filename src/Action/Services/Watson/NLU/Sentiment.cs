using System;
using System.Collections.Generic;

namespace Action.Services.Watson.NLU
{
    public class Sentiment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public SentimentDocument SentimentDoc { get; set; }
        public string RetrievedUrl { get; set; }
        public List<SentimentTarget> SentimentTarget { get; set; }
    }
}