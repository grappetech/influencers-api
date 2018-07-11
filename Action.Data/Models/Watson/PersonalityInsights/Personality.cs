﻿using System;
using System.Collections.Generic;

namespace Action.Data.Models.Core.Watson.PersonalityInsights
{
    public class Personality
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double? Percentile { get; set; }
        public List<Detail> Details { get; set; }
    }
}