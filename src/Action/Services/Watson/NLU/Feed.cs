using System;

namespace Action.Services.Watson.NLU
{
    public class Feed
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string link { get; set; }
    }
}