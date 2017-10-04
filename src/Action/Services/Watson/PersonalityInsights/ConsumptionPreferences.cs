using System;
using System.Collections.Generic;

namespace Action.Services.Watson.PersonalityInsights
{
    public class ConsumptionPreferences
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ConsumptionPreferenceId { get; set; }
        public string Name { get; set; }
        public List<ConsumptionDetail> ConsumptionDetails { get; set; }
    }

}
