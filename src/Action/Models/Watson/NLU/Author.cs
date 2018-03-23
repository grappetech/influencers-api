using System;

namespace Action.Models.Watson.NLU
{
    public class Author
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string name { get; set; }
    }
}