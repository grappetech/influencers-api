﻿using System;

namespace Action.Data.Models.Watson.NLU
{
    public class SentimentDocument
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public float? Score { get; set; }
        public string Label { get; set; }
    }
}