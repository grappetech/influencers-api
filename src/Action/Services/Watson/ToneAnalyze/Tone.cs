using System;

namespace Action.Services.Watson.ToneAnalyze
{
    public class Tone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double? Score { get; set; }
        public string ToneId { get; set; }
        public string ToneName { get; set; }
    }

}
