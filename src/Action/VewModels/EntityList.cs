using System.Collections.Generic;

namespace Action.VewModels
{
    public class EntityList
    {
        public virtual List<SimpleEntity> Entities { get; set; } = new List<SimpleEntity>();
    }
}