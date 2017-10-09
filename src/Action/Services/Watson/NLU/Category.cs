﻿using System;

namespace Action.Services.Watson.NLU
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public float? Score { get; set; }
        public string Label { get; set; }
    }
}