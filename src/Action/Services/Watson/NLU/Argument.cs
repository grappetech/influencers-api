using System;
using System.Collections.Generic;

namespace Action.Services.Watson.NLU
{
    public class Argument
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string text { get; set; }
        private List<EntityRelation> _entityRelations = new List<EntityRelation>();
        public List<EntityRelation> EntityRelations
        {
            get { return _entityRelations; }
            set { _entityRelations = value; }
        }
    }

}