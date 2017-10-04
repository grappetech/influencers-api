using System;
using System.Collections.Generic;

namespace Action.Services.Watson.NLU
{
    public class Disambiguation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        private List<DisambiguationSubtype> _Subtype = new List<DisambiguationSubtype>();
        public List<DisambiguationSubtype> Subtype
        {
            get { return _Subtype; }
            set { _Subtype = value; }
        }

        public string Name { get; set; }
        public string Dbpedia_resource { get; set; }
    }


}
