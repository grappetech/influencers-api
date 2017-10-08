namespace Action.VewModels
{
    public class AnalyseRequest
    {
        public string Brand { get; set; }
        public string Product { get; set; }
        public string Briefing { get; set; }
        public string Factor { get; set; }
        public byte[] File { get; set; }
    }
}