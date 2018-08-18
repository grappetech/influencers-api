using System;
using CoreEntity = Action.Data.Models.Core.Watson.Entity;
namespace Action.Data.Models.Watson.NLU
{
    public class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; }
        public string Text { get; set; }
        public float? Relevance { get; set; }
        public Disambiguation Disambiguation { get; set; }
        public long? Count { get; set; }
        public long? EntityId { get; set; }
        public CoreEntity CoreEntity { get; set; }
    }
}