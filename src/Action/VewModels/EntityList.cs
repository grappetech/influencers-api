using System.Collections.Generic;
using Newtonsoft.Json;

namespace Action.VewModels
{
    public class EntityList
    {
        [JsonProperty("entities")]
        public virtual List<SimpleEntity> Entities { get; set; } = new List<SimpleEntity>();
    }
}