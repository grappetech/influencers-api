using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Action.Models.Core;

namespace Action.Models.Watson
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public ECategory CategoryId { get; set; }
        public string Category => Enum.GetName(typeof(ECategory), CategoryId);
        public DateTime Date { get; set; } = DateTime.Today;
        public string FacebookUser { get; set; }
        public string TweeterUser { get; set; }
        public string InstagranUser { get; set; }
        public string YoutubeUser { get; set; }
        public string PictureUrl { get; set; }
        public string SiteUrl { get; set; }
        public int Tier { get; set; } = 3;
        public ICollection<Briefing> Briefings { get; set; } = new List<Briefing>();
    }
}