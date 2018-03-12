using System;
using Action.Models;

namespace Action.VewModels
{
    public class BriefingViewModel
    {
        public long Id { get; set; }
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
        public decimal Strength { get; set; }
        public string DocumentUrl { get; set; }
        public EStatus? Status { get; set; }
        public DateTime? Date { get; set; }
    }
}