using System;
using System.Collections.Generic;

namespace Action.Services.Watson.NLU
{
    public partial class Metadata
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string language { get; set; }
        public string retrieved_url { get; set; }
        public string title { get; set; }
        public DateTime publication_date { get; set; }
        public string image { get; set; }

        private List<Feed> _feeds = new List<Feed>();
        public List<Feed> Feeds
        {
            get { return _feeds; }
            set { _feeds = value; }
        }

        private List<Author> _authors = new List<Author>();
        public List<Author> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }
    }
}
