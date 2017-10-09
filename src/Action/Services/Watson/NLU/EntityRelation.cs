using System;

namespace Action.Services.Watson.NLU
{
    public class EntityRelation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string type { get; set; }
        public string text { get; set; }
    }
}