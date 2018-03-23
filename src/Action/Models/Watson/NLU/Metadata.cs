using System;
using System.Collections.Generic;

namespace Action.Models.Watson.NLU
{
    public class Metadata
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string language { get; set; }
        public string retrieved_url { get; set; }
        public string title { get; set; }
        public DateTime publication_date { get; set; }
        public string image { get; set; }

        public List<Feed> Feeds { get; set; } = new List<Feed>();

        public List<Author> Authors { get; set; } = new List<Author>();
    }
}