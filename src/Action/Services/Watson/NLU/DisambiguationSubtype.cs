﻿using System;

namespace Action.Services.Watson.NLU
{
    public class DisambiguationSubtype
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Disambiguation Disambiguation { get; set; }
        public string SubType { get; set; }
    }
}