using System;

namespace Action.Services.Watson.NLU
{
    public class SemanticSubject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
    }
}
