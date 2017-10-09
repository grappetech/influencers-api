using System;

namespace Action.Services.Watson.NLU
{
    public class SemanticObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
    }
}