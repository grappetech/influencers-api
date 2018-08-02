﻿using System;

namespace Action.Data.Models.Watson.NLU
{
    public class SemanticVerb
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public string Tense { get; set; }
    }
}