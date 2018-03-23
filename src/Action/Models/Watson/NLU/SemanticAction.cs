using System;

namespace Action.Models.Watson.NLU
{
    public class SemanticAction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public SemanticVerb Verb { get; set; }
        public string Text { get; set; }
        public string Normalized { get; set; }
    }
}