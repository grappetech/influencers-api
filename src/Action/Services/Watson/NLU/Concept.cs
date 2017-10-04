using System;

namespace Action.Services.Watson.NLU
{
    public class Concept
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public float? Relevance { get; set; }
        public string Dbpedia_resource { get; set; }
    }
}
