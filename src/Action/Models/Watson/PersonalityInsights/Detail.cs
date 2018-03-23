using System;

namespace Action.Models.Watson.PersonalityInsights
{
    public class Detail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double? Percentile { get; set; }
    }
}