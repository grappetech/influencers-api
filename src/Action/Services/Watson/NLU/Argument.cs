using System;
using System.Collections.Generic;

namespace Action.Services.Watson.NLU
{
    public class Argument
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string text { get; set; }

        public List<EntityRelation> EntityRelations { get; set; } = new List<EntityRelation>();
    }
}