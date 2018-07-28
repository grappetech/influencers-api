using System;

namespace Action.Data.Models.Watson.NLU
{
    public class SemanticRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public SemanticSubject Subject { get; set; }
        public string Sentence { get; set; }
        public SemanticObject Object { get; set; }
        public SemanticAction Action { get; set; }
    }
}