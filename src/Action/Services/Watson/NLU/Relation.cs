using System;
using System.Collections.Generic;

namespace Action.Services.Watson.NLU
{
    public class Relation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string language { get; set; }
        public string type { get; set; }
        public string sentence { get; set; }
        public float? score { get; set; }

        public List<Argument> Arguments { get; set; } = new List<Argument>();
    }
}