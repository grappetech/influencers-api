using System;
using System.Collections.Generic;

namespace Action.Data.Models.Watson.NLU
{
    public class Disambiguation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public List<DisambiguationSubtype> Subtype { get; set; } = new List<DisambiguationSubtype>();

        public string Name { get; set; }
        public string Dbpedia_resource { get; set; }
    }
}