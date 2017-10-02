using System;

namespace Action.Models.Watson
{
    public class Entity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}