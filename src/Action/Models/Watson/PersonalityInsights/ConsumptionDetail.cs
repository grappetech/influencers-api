using System;

namespace Action.Models.Watson.PersonalityInsights
{
    public class ConsumptionDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Score { get; set; }
    }
}