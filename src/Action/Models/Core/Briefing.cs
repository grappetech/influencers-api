using System;
using System.ComponentModel.DataAnnotations;
using Action.Models.Watson;

namespace Action.Models.Core
{
    public class Briefing
    {
        
        [Key]
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string AgeRange { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Personality { get; set; }
        public string Value { get; set; }
        public string Tone { get; set; }
        public string Description { get; set; }
        public string Factor { get; set; }
        public string Entity { get; set; }
        public string Analysis { get; set; }
        public decimal Strength { get; set; }
        public string DocumentUrl { get; set; }
        public int? ConnectedEntityId { get; set; }
        public Entity ConnectedEntity { get; set; }
        public string Report { get; set; }
        public string Report2 { get; set; }
        public EStatus? Status { get; set; }
        public DateTime? Date { get; set; } = DateTime.Today;
    }
}