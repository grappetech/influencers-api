﻿using System;

namespace Action.Data.Models.Watson.NLU
{
    public class SentimentKeyword
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double score { get; set; }
    }
}